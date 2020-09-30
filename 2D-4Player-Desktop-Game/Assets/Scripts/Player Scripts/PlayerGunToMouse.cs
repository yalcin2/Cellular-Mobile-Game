using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunToMouse : Photon.MonoBehaviour {
    
    public PlayerMove playerMove;
    private int rotationOffset = 0;
    private Quaternion gunPos;
    private Vector3 difference;

    // Update is called once per frame
    void Update () {
        if (playerMove.photonView.isMine)
        {
            RotateArm();
        }
        else
        {
            SmoothNetMovement();
        }

	}

    private void RotateArm()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        //Quaternion rotation = Quaternion.AngleAxis(rotZ , Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
    }

    private void SmoothNetMovement()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, gunPos, Time.deltaTime * 8);
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.rotation);
        }
        else
        {
            gunPos = (Quaternion)stream.ReceiveNext();
        }
    }

}

