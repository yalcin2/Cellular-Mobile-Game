using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonHandler : Photon.MonoBehaviour {

    public GameObject mainPlayer;

    public bool checkPlayer = true;

    private void Awake()
    {
        //DontDestroyOnLoad(this.transform);
        PhotonNetworkManager.sendRate = 30;
        PhotonNetworkManager.sendRateOnSerialize = 20;

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public void MoveScene()
    {
        PhotonNetworkManager.LoadLevel("GameScene");
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "GameScene")
        {
            if (checkPlayer == true)
            {
                SpawnPlayer();
                checkPlayer = false;
            }
            else
                return;
        }
    }

    private void SpawnPlayer()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        PhotonNetworkManager.Instantiate(mainPlayer.name, spawnPoints[randomSpawnPoint].transform.position, mainPlayer.transform.rotation, 0);
    }
}
