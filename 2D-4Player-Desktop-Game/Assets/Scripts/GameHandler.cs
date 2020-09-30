using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : Photon.MonoBehaviour {

    public static GameHandler Instance;

    [HideInInspector] public GameObject localPlayer;
    public GameObject respawnCanvas;
    private GameObject[] spawnPoints;

    public Text timerText;

    [SerializeField] private float timerAmount = 5f;

    private bool enableTimer = false;

    public bool char1;
    public bool char2;
    public bool char3;
    public bool char4;

    public string player1;
    public string player2;
    public string player3;
    public string player4;

    public static GameObject[] players;

    private void Awake()
    {
        char1 = false;
        char2 = false;
        char3 = false;
        char4 = false;

        Instance = this;
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    void Update()
    {
        if (enableTimer)
        {
            timerAmount -= Time.deltaTime;
            timerText.text = "Respawning in: " + timerAmount.ToString("F0");

            if(timerAmount <= 0)
            {
                localPlayer.GetComponent<PhotonView>().RPC("PlayerAliveRespawn", PhotonTargets.AllBuffered);
                respawnCanvas.SetActive(false);
                enableTimer = false;
                int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                localPlayer.transform.position = spawnPoints[randomSpawnPoint].transform.position;
            }
        }

        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void StartTimer()
    {
        timerAmount = 10f;
        respawnCanvas.SetActive(true);
        enableTimer = true;
    }


    public void OnClickGoBack()
    {
        PhotonNetworkManager.LeaveRoom();
        PhotonNetworkManager.LoadLevel("LobbyScene");
    }

    public void OnJoinRoom()
    {
        
    }




}   

