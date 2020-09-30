using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : Photon.MonoBehaviour
{
    public GameObject sectionView1;

    public void ConnectToPhoton()
    {
        PhotonNetworkManager.ConnectUsingSettings("0.1");
        Debug.Log("We have now joined the lobby");

        PhotonNetworkManager.LoadLevel("LobbyScene");
    }

    public void OnConnectedToMaster()
    {
        PhotonNetworkManager.JoinLobby(TypedLobby.Default);

        Debug.Log("We are connected to master");
    }

}
