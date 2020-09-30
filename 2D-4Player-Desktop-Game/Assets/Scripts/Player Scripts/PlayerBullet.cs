using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Photon.MonoBehaviour {

    public GameObject bullet;

    public bool movingDirection = false;

    public float movingSpeed = 35f;

    public float destroyTime = 2f;

    [PunRPC]
    public void ChangeDirection_Left()
    {
        movingDirection = false;
    }

    void Update()
    {
        if (!movingDirection)
        {
            transform.Translate(Vector2.right * movingSpeed * Time.deltaTime);
            Object.Destroy(bullet, destroyTime);
        }
        else
            transform.Translate(Vector2.left * movingSpeed * Time.deltaTime);
            Object.Destroy(bullet, destroyTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!photonView.isMine)
        {
            return;
        }

        PhotonView target = other.gameObject.GetComponent<PhotonView>();

        if(target != null && (!target.isMine || target.isSceneView))
            if(other.tag == "Player")
            {
                other.GetComponent<PhotonView>().RPC("ReduceHealth", PhotonTargets.All);
                this.GetComponent<PhotonView>().RPC("DestroyOBJ", PhotonTargets.All);
            }

    }

    [PunRPC]
    private void DestroyOBJ()
    {
        Destroy(bullet);
    }

}

