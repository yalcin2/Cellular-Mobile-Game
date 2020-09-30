using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{

    public static int playerWhoIsIt = 0;
    private static PhotonView ScenePhotonView;

    // Use this for initialization
    public void Start()
    {
        ScenePhotonView = this.GetComponent<PhotonView>();
    }

    public void OnJoinedRoom()
    {
        // game logic: if this is the only player, we're "it"
        if (PhotonNetworkManager.playerList.Length == 1)
        {
            playerWhoIsIt = PhotonNetworkManager.player.ID;
        }

        Debug.Log("playerWhoIsIt: " + playerWhoIsIt);
    }

    public void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        Debug.Log("OnPhotonPlayerConnected: " + player);

        // when new players join, we send "who's it" to let them know
        // only one player will do this: the "master"

        if (PhotonNetworkManager.isMasterClient)
        {
            TagPlayer(playerWhoIsIt);
        }
    }

    public static void TagPlayer(int playerID)
    {
        Debug.Log("TagPlayer: " + playerID);
        ScenePhotonView.RPC("TaggedPlayer", PhotonTargets.All, playerID);
    }

    [PunRPC]
    public void TaggedPlayer(int playerID)
    {
        playerWhoIsIt = playerID;
        Debug.Log("TaggedPlayer: " + playerID);
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        Debug.Log("OnPhotonPlayerDisconnected: " + player);

        if (PhotonNetworkManager.isMasterClient)
        {
            if (player.ID == playerWhoIsIt)
            {
                // if the player who left was "it", the "master" is the new "it"
                TagPlayer(PhotonNetworkManager.player.ID);
            }
        }
    }

    public void OnMasterClientSwitched()
    {
        Debug.Log("OnMasterClientSwitched");
    }
}
