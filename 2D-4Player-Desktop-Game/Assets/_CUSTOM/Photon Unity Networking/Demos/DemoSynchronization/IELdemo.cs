using ExitGames.Client.Photon;
using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

/// <summary>
/// A minimal UI to show connection info in a demo.
/// </summary>
public class IELdemo : MonoBehaviour
{
    public GUISkin Skin;

    public void OnGUI()
    {
        if (this.Skin != null)
        {
            GUI.skin = this.Skin;
        }

        if (PhotonNetworkManager.isMasterClient)
        {
            GUILayout.Label("Controlling client.\nPing: " + PhotonNetworkManager.GetPing());
            if (GUILayout.Button("disconnect", GUILayout.ExpandWidth(false)))
            {
                PhotonNetworkManager.Disconnect();
            }
        }
        else if (PhotonNetworkManager.isNonMasterClientInRoom)
        {
            GUILayout.Label("Receiving updates.\nPing: " + PhotonNetworkManager.GetPing());
            if (GUILayout.Button("disconnect", GUILayout.ExpandWidth(false)))
            {
                PhotonNetworkManager.Disconnect();
            }
        }
        else
        {
            GUILayout.Label("Not in room yet\n" + PhotonNetworkManager.connectionStateDetailed);
        }
        if (!PhotonNetworkManager.connected && !PhotonNetworkManager.connecting)
        {
            if (GUILayout.Button("connect", GUILayout.Width(80)))
            {
                PhotonNetworkManager.ConnectUsingSettings(null);   // using null as parameter, re-uses previously set version.
            }
        }
    }
}
