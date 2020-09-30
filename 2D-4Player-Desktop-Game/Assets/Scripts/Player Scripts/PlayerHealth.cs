using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Photon.MonoBehaviour {

    public PlayerMove playerMove;

    public GameObject mainPlayer;
    public SpriteRenderer characterModel;
    public SpriteRenderer gunModel;

    public GameObject WorldSpace_Canvas;
    public GameObject LocalPlayer_Canvas;
    public GameObject OtherPlayer_Canvas;

    public Image LocalPlayer_Healthbar;
    public Image OtherPlayer_HealthBar;

    public Vector3 LocalPlayerName_POS;
    public Vector3 OtherPlayerName_POS;

    private bool ready = true;

    public GameObject tempSpawnObj;

    void Awake()
    {
        SetCorrectCanvas();
        if (photonView.isMine)
        {
            GameHandler.Instance.localPlayer = this.gameObject;
        }
    }

        void SetCorrectCanvas()
    {
        if (photonView.isMine)
        {
            playerMove.plName.GetComponent<RectTransform>().anchoredPosition = (LocalPlayerName_POS);
            LocalPlayer_Canvas.SetActive(true);

        }
        else
        {
            playerMove.plName.GetComponent<RectTransform>().anchoredPosition = (OtherPlayerName_POS);
            OtherPlayer_Canvas.SetActive(true);
        }

    }

    [PunRPC]
    public void PlayerDeadRespawn()
    {
        characterModel.enabled = false;
        gunModel.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider2D>().enabled = false;

        if (!photonView.isMine)
        {
            OtherPlayer_Canvas.SetActive(false);
        }
        WorldSpace_Canvas.SetActive(false);
    }

    [PunRPC]
    public void PlayerAliveRespawn()
    {
        characterModel.GetComponent<SpriteRenderer>().enabled = true;
        gunModel.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<BoxCollider2D>().enabled = true;

        if (photonView.isMine)
        {
            LocalPlayer_Healthbar.fillAmount = 1;
        }
        else
        {
            OtherPlayer_Canvas.SetActive(true);
            OtherPlayer_HealthBar.fillAmount = 1;
        }
        WorldSpace_Canvas.SetActive(true);
    }

    [PunRPC]    
    public void ReduceHealth()
    {
        ReduceHealthAmount(0.1f);
    }

    public void ReduceHealthAmount(float hit)
    {
        if (photonView.isMine)
        {
            LocalPlayer_Healthbar.fillAmount -= hit;
            CheckHealthAmount();
        }
        else
        {
            OtherPlayer_HealthBar.fillAmount -= hit;
        }
    }

    private void CheckHealthAmount()
    {
        if(LocalPlayer_Healthbar.fillAmount <= 0.01f)
        {
            GameHandler.Instance.StartTimer();
            this.GetComponent<PhotonView>().RPC("PlayerDeadRespawn", PhotonTargets.AllBuffered);
            mainPlayer.transform.position = tempSpawnObj.transform.position;

        }
    }


    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(LocalPlayer_Healthbar.fillAmount);
        }
        else
        {
            OtherPlayer_HealthBar.fillAmount = (float)stream.ReceiveNext();
        }
    }

}
