using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using ExitGames.Client.Photon;

public class RpsDemoConnect : PunBehaviour
{
    public InputField InputField;
    public string UserId;

	string previousRoomPlayerPrefKey = "PUN:Demo:RPS:PreviousRoom";
	public string previousRoom;

    private const string MainSceneName = "DemoRPS-Scene";

	const string NickNamePlayerPrefsKey = "NickName";


	void Start()
	{
		InputField.text = PlayerPrefs.HasKey(NickNamePlayerPrefsKey)?PlayerPrefs.GetString(NickNamePlayerPrefsKey):"";
	}

    public void ApplyUserIdAndConnect()
    {
		string nickName = "DemoNick";
        if (this.InputField != null && !string.IsNullOrEmpty(this.InputField.text))
        {
            nickName = this.InputField.text;
			PlayerPrefs.SetString(NickNamePlayerPrefsKey,nickName);
        }
        //if (string.IsNullOrEmpty(UserId))
        //{
        //    this.UserId = nickName + "ID";
        //}
    
        if (PhotonNetworkManager.AuthValues == null)
        {
            PhotonNetworkManager.AuthValues = new AuthenticationValues();
        }
        //else
        //{
        //    Debug.Log("Re-using AuthValues. UserId: " + PhotonNetwork.AuthValues.UserId);
        //}


		PhotonNetworkManager.AuthValues.UserId = nickName;

		Debug.Log("Nickname: " + nickName + " userID: " + this.UserId,this);
		


        PhotonNetworkManager.playerName = nickName;
        PhotonNetworkManager.ConnectUsingSettings("0.5");
        
        // this way we can force timeouts by pausing the client (in editor)
        PhotonMenuManager.StopFallbackSendAckThread();
    }


    public override void OnConnectedToMaster()
    {
        // after connect 
        this.UserId = PhotonNetworkManager.player.UserId;
        ////Debug.Log("UserID " + this.UserId);

		if (PlayerPrefs.HasKey(previousRoomPlayerPrefKey))
		{
			Debug.Log("getting previous room from prefs: ");
			this.previousRoom = PlayerPrefs.GetString(previousRoomPlayerPrefKey);
			PlayerPrefs.DeleteKey(previousRoomPlayerPrefKey); // we don't keep this, it was only for initial recovery
		}


        // after timeout: re-join "old" room (if one is known)
        if (!string.IsNullOrEmpty(this.previousRoom))
        {
            Debug.Log("ReJoining previous room: " + this.previousRoom);
            PhotonNetworkManager.ReJoinRoom(this.previousRoom);
            this.previousRoom = null;       // we only will try to re-join once. if this fails, we will get into a random/new room
        }
        else
        {
            // else: join a random room
            PhotonNetworkManager.JoinRandomRoom();
        }
    }

    public override void OnJoinedLobby()
    {
        OnConnectedToMaster(); // this way, it does not matter if we join a lobby or not
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
		Debug.Log("OnPhotonRandomJoinFailed");
        PhotonNetworkManager.CreateRoom(null, new RoomOptions() { MaxPlayers = 2, PlayerTtl = 20000 }, null);
    }

    public override void OnJoinedRoom()
    {
		Debug.Log("Joined room: " + PhotonNetworkManager.room.Name);
        this.previousRoom = PhotonNetworkManager.room.Name;
		PlayerPrefs.SetString(previousRoomPlayerPrefKey,this.previousRoom);

    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
		Debug.Log("OnPhotonJoinRoomFailed");
        this.previousRoom = null;
		PlayerPrefs.DeleteKey(previousRoomPlayerPrefKey);
    }

    public override void OnConnectionFail(DisconnectCause cause)
    {
        Debug.Log("Disconnected due to: " + cause + ". this.previousRoom: " + this.previousRoom);
    }
	
	public override void OnPhotonPlayerActivityChanged(PhotonPlayer otherPlayer)
	{
		Debug.Log("OnPhotonPlayerActivityChanged() for "+otherPlayer.NickName+" IsInactive: "+otherPlayer.IsInactive);
	}

}
