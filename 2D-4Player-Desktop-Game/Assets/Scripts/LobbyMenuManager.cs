using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMenuManager : Photon.MonoBehaviour {

    public GameObject sectionView2, sectionView3;
    public Text serverList;

    void Awake()
    {
        serverList.text = "";
        checkServerList();
    }

    void Start()
    {
        checkServerList();
    }

    void Update()
    {
    }

    public void refreshServerList()
    {
        serverList.text = "";
        RoomInfo[] rooms = PhotonNetworkManager.GetRoomList();
        for (var i = 0; i < rooms.Length; i++)
        {
            serverList.text = serverList.text + rooms[i].ToString();
        }
    }

    public void checkServerList()
    {
        serverList.text = "";
        RoomInfo[] rooms = PhotonNetworkManager.GetRoomList();
        for (var i = 0; i < rooms.Length; i++)
        {
            serverList.text = serverList.text + rooms[i].ToString();
        }
    }

    public void ConnectToPhoton()
    {
        PhotonNetworkManager.ConnectUsingSettings("0.01");
        Debug.Log("We have now joined the lobby");

        PhotonNetworkManager.LoadLevel("LobbyScene");
    }

    public void OnConnectedToMaster()
    {
        PhotonNetworkManager.JoinLobby(TypedLobby.Default);

        Debug.Log("We are connected to master");
    }

    public virtual void OnJoinedLobby()
    {
        sectionView3.SetActive(false);
        sectionView2.SetActive(true);
        Debug.Log("Joined Lobby");
    }

    public virtual void OnDisconnectedFromPhoton()
    {
        sectionView2.SetActive(false);
        sectionView3.SetActive(true);

        Debug.Log("Disconnected from lobby");
    }

    private void OnFailedToConnectToPhoton()
    {
        sectionView2.SetActive(false);
        sectionView3.SetActive(true);

        Debug.Log("Failed to connect to the lobby");
    }
}
