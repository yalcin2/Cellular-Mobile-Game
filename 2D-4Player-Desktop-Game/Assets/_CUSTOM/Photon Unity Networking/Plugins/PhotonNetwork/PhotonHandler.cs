// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhotonHandler.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Diagnostics;
using ExitGames.Client.Photon;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using SupportClassPun = ExitGames.Client.Photon.SupportClass;

#if UNITY_5_5_OR_NEWER
using UnityEngine.Profiling;
#endif


#if UNITY_WEBGL
#pragma warning disable 0649
#endif

/// <summary>
/// Internal Monobehaviour that allows Photon to run an Update loop.
/// </summary>
internal class PhotonMenuManager : MonoBehaviour
{
    public static PhotonMenuManager SP;

    public int updateInterval;  // time [ms] between consecutive SendOutgoingCommands calls

    public int updateIntervalOnSerialize;  // time [ms] between consecutive RunViewUpdate calls (sending syncs, etc)

    private int nextSendTickCount = 0;

    private int nextSendTickCountOnSerialize = 0;
	
    private static bool sendThreadShouldRun;

    private static Stopwatch timerToStopConnectionInBackground;

    protected internal static bool AppQuits;

    protected internal static Type PingImplementation = null;

    protected void Awake()
    {
        if (SP != null && SP != this && SP.gameObject != null)
        {
            GameObject.DestroyImmediate(SP.gameObject);
        }

        SP = this;
        DontDestroyOnLoad(this.gameObject);

        this.updateInterval = 1000 / PhotonNetworkManager.sendRate;
        this.updateIntervalOnSerialize = 1000 / PhotonNetworkManager.sendRateOnSerialize;

        PhotonMenuManager.StartFallbackSendAckThread();
    }


	#if UNITY_5_4_OR_NEWER

    protected void Start()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, loadingMode) =>
        {
            PhotonNetworkManager.networkingPeer.NewSceneLoaded();
            PhotonNetworkManager.networkingPeer.SetLevelInPropsIfSynced(SceneManagerHelper.ActiveSceneName);
        };
    }

    #else

    /// <summary>Called by Unity after a new level was loaded.</summary>
    protected void OnLevelWasLoaded(int level)
    {
        PhotonNetwork.networkingPeer.NewSceneLoaded();
        PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced(SceneManagerHelper.ActiveSceneName);
    }

    #endif


    /// <summary>Called by Unity when the application is closed. Disconnects.</summary>
    protected void OnApplicationQuit()
    {
        PhotonMenuManager.AppQuits = true;
        PhotonMenuManager.StopFallbackSendAckThread();
        PhotonNetworkManager.Disconnect();
    }

    /// <summary>
    /// Called by Unity when the application gets paused (e.g. on Android when in background).
    /// </summary>
    /// <remarks>
    /// Sets a disconnect timer when PhotonNetwork.BackgroundTimeout > 0.1f. See PhotonNetwork.BackgroundTimeout.
    ///
    /// Some versions of Unity will give false values for pause on Android (and possibly on other platforms).
    /// </remarks>
    /// <param name="pause">If the app pauses.</param>
    protected void OnApplicationPause(bool pause)
    {
        if (PhotonNetworkManager.BackgroundTimeout > 0.1f)
        {
            if (timerToStopConnectionInBackground == null)
            {
                timerToStopConnectionInBackground = new Stopwatch();
            }
            timerToStopConnectionInBackground.Reset();

            if (pause)
            {
                timerToStopConnectionInBackground.Start();
            }
            else
            {
                timerToStopConnectionInBackground.Stop();
            }
        }
    }

    /// <summary>Called by Unity when the play mode ends. Used to cleanup.</summary>
    protected void OnDestroy()
    {
        //Debug.Log("OnDestroy on PhotonHandler.");
        PhotonMenuManager.StopFallbackSendAckThread();
        //PhotonNetwork.Disconnect();
    }

    protected void Update()
    {
        if (PhotonNetworkManager.networkingPeer == null)
        {
            Debug.LogError("NetworkPeer broke!");
            return;
        }

        if (PhotonNetworkManager.connectionStateDetailed == ClientState.PeerCreated || PhotonNetworkManager.connectionStateDetailed == ClientState.Disconnected || PhotonNetworkManager.offlineMode)
        {
            return;
        }

        // the messageQueue might be paused. in that case a thread will send acknowledgements only. nothing else to do here.
        if (!PhotonNetworkManager.isMessageQueueRunning)
        {
            return;
        }

        bool doDispatch = true;
        while (PhotonNetworkManager.isMessageQueueRunning && doDispatch)
        {
            // DispatchIncomingCommands() returns true of it found any command to dispatch (event, result or state change)
            Profiler.BeginSample("DispatchIncomingCommands");
            doDispatch = PhotonNetworkManager.networkingPeer.DispatchIncomingCommands();
            Profiler.EndSample();
        }

        int currentMsSinceStart = (int)(Time.realtimeSinceStartup * 1000);  // avoiding Environment.TickCount, which could be negative on long-running platforms
        if (PhotonNetworkManager.isMessageQueueRunning && currentMsSinceStart > this.nextSendTickCountOnSerialize)
        {
            PhotonNetworkManager.networkingPeer.RunViewUpdate();
            this.nextSendTickCountOnSerialize = currentMsSinceStart + this.updateIntervalOnSerialize;
            this.nextSendTickCount = 0;     // immediately send when synchronization code was running
        }

        currentMsSinceStart = (int)(Time.realtimeSinceStartup * 1000);
        if (currentMsSinceStart > this.nextSendTickCount)
        {
            bool doSend = true;
            while (PhotonNetworkManager.isMessageQueueRunning && doSend)
            {
                // Send all outgoing commands
                Profiler.BeginSample("SendOutgoingCommands");
                doSend = PhotonNetworkManager.networkingPeer.SendOutgoingCommands();
                Profiler.EndSample();
            }

            this.nextSendTickCount = currentMsSinceStart + this.updateInterval;
        }
    }

    protected void OnJoinedRoom()
    {
        PhotonNetworkManager.networkingPeer.LoadLevelIfSynced();
    }

    protected void OnCreatedRoom()
    {
        PhotonNetworkManager.networkingPeer.SetLevelInPropsIfSynced(SceneManagerHelper.ActiveSceneName);
    }

    public static void StartFallbackSendAckThread()
    {
	    #if !UNITY_WEBGL
        if (sendThreadShouldRun)
        {
            return;
        }

        sendThreadShouldRun = true;
        SupportClassPun.StartBackgroundCalls(FallbackSendAckThread);   // thread will call this every 100ms until method returns false
	    #endif
    }

    public static void StopFallbackSendAckThread()
    {
	    #if !UNITY_WEBGL
        sendThreadShouldRun = false;
	    #endif
    }

    /// <summary>A thread which runs independent from the Update() calls. Keeps connections online while loading or in background. See PhotonNetwork.BackgroundTimeout.</summary>
    public static bool FallbackSendAckThread()
    {
        if (sendThreadShouldRun && !PhotonNetworkManager.offlineMode && PhotonNetworkManager.networkingPeer != null)
        {
            // check if the client should disconnect after some seconds in background
            if (timerToStopConnectionInBackground != null && PhotonNetworkManager.BackgroundTimeout > 0.1f)
            {
                if (timerToStopConnectionInBackground.ElapsedMilliseconds > PhotonNetworkManager.BackgroundTimeout * 1000)
                {
                    if (PhotonNetworkManager.connected)
                    {
                        PhotonNetworkManager.Disconnect();
                    }
                    timerToStopConnectionInBackground.Stop();
                    timerToStopConnectionInBackground.Reset();
                    return sendThreadShouldRun;
                }
            }

            if (!PhotonNetworkManager.isMessageQueueRunning || PhotonNetworkManager.networkingPeer.ConnectionTime - PhotonNetworkManager.networkingPeer.LastSendOutgoingTime > 200)
            {
                PhotonNetworkManager.networkingPeer.SendAcksOnly();
            }
        }

        return sendThreadShouldRun;
    }


    #region Photon Cloud Ping Evaluation


    private const string PlayerPrefsKey = "PUNCloudBestRegion";

    internal static CloudRegionCode BestRegionCodeInPreferences
    {
        get
        {
            string prefsRegionCode = PlayerPrefs.GetString(PlayerPrefsKey, "");
            if (!string.IsNullOrEmpty(prefsRegionCode))
            {
                CloudRegionCode loadedRegion = Region.Parse(prefsRegionCode);
                return loadedRegion;
            }

            return CloudRegionCode.none;
        }
        set
        {
            if (value == CloudRegionCode.none)
            {
                PlayerPrefs.DeleteKey(PlayerPrefsKey);
            }
            else
            {
                PlayerPrefs.SetString(PlayerPrefsKey, value.ToString());
            }
        }
    }


    internal protected static void PingAvailableRegionsAndConnectToBest()
    {
        SP.StartCoroutine(SP.PingAvailableRegionsCoroutine(true));
    }


    internal IEnumerator PingAvailableRegionsCoroutine(bool connectToBest)
    {
        while (PhotonNetworkManager.networkingPeer.AvailableRegions == null)
        {
            if (PhotonNetworkManager.connectionStateDetailed != ClientState.ConnectingToNameServer && PhotonNetworkManager.connectionStateDetailed != ClientState.ConnectedToNameServer)
            {
                Debug.LogError("Call ConnectToNameServer to ping available regions.");
                yield break; // break if we don't connect to the nameserver at all
            }

            Debug.Log("Waiting for AvailableRegions. State: " + PhotonNetworkManager.connectionStateDetailed + " Server: " + PhotonNetworkManager.Server + " PhotonNetwork.networkingPeer.AvailableRegions " + (PhotonNetworkManager.networkingPeer.AvailableRegions != null));
            yield return new WaitForSeconds(0.25f); // wait until pinging finished (offline mode won't ping)
        }

        if (PhotonNetworkManager.networkingPeer.AvailableRegions == null || PhotonNetworkManager.networkingPeer.AvailableRegions.Count == 0)
        {
            Debug.LogError("No regions available. Are you sure your appid is valid and setup?");
            yield break; // break if we don't get regions at all
        }

        PhotonPingManager pingManager = new PhotonPingManager();
        foreach (Region region in PhotonNetworkManager.networkingPeer.AvailableRegions)
        {
            SP.StartCoroutine(pingManager.PingSocket(region));
        }

        while (!pingManager.Done)
        {
            yield return new WaitForSeconds(0.1f); // wait until pinging finished (offline mode won't ping)
        }


        Region best = pingManager.BestRegion;
        PhotonMenuManager.BestRegionCodeInPreferences = best.Code;

        Debug.Log("Found best region: '" + best.Code + "' ping: " + best.Ping + ". Calling ConnectToRegionMaster() is: " + connectToBest);

        if (connectToBest)
        {
            PhotonNetworkManager.networkingPeer.ConnectToRegionMaster(best.Code);
        }
    }



    #endregion

}
