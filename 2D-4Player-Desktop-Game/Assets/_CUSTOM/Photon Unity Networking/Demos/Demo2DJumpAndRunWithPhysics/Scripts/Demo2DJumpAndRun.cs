using UnityEngine;
using System.Collections;

public class Demo2DJumpAndRun : MonoBehaviour 
{
    void OnJoinedRoom()
    {
        if( PhotonNetworkManager.isMasterClient == false )
        {
            return;
        }

        PhotonNetworkManager.InstantiateSceneObject( "Physics Box", new Vector3( -4.5f, 5.5f, 0 ), Quaternion.identity, 0, null );
        PhotonNetworkManager.InstantiateSceneObject( "Physics Box", new Vector3( -4.5f, 4.5f, 0 ), Quaternion.identity, 0, null );
        PhotonNetworkManager.InstantiateSceneObject( "Physics Box", new Vector3( -4.5f, 3.5f, 0 ), Quaternion.identity, 0, null );

        PhotonNetworkManager.InstantiateSceneObject( "Physics Box", new Vector3( 4.5f, 5.5f, 0 ), Quaternion.identity, 0, null );
        PhotonNetworkManager.InstantiateSceneObject( "Physics Box", new Vector3( 4.5f, 4.5f, 0 ), Quaternion.identity, 0, null );
        PhotonNetworkManager.InstantiateSceneObject( "Physics Box", new Vector3( 4.5f, 3.5f, 0 ), Quaternion.identity, 0, null );
    }
}
