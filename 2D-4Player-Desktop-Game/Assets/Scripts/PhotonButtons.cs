using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonButtons : Photon.MonoBehaviour {

    public PhotonHandler phandler;

    public InputField createRoomInput, joinRoomInput;

    public void OnClickCreateRoom()
    {
        if (createRoomInput.text.Length >= 1)
            PhotonNetworkManager.CreateRoom(createRoomInput.text, new RoomOptions() { MaxPlayers = 6 }, null);
    }

    public void OnClickJoinRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetworkManager.JoinOrCreateRoom(joinRoomInput.text, roomOptions, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        phandler.MoveScene();
        Debug.Log("We are connected to the room!");
    }
}
