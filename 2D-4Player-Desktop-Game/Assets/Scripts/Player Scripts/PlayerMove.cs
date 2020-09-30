using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Controller2D))]
public class PlayerMove : Photon.MonoBehaviour {

    [Header("Player Objects")]
    public GameObject mainPlayer;
    public GameObject bulletObject;

    [Header("Network Objects")]
    public new PhotonView photonView;
    public Text plName;
    private Vector3 selfpos;
    private Vector3 playerScreenPoint;

    [Header("Sprites")]
    //public SpriteRenderer[] characterModels = new SpriteRenderer[4];
    public SpriteRenderer characterModel;
    public SpriteRenderer gunModel;

    public SpriteRenderer char1;
    public SpriteRenderer char2;
    public SpriteRenderer char3;
    public SpriteRenderer char4;

    int randomChar;
    int playerAmount;

    [Header("Player Movement")]
    public float maxJumpHeight = 3;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .25f;
    float timeToWallUnstick;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;

    bool loopOnce;

    private void Awake()
    {
        loopOnce = true;

        if (photonView.isMine)
            //characterModel = GetComponent<SpriteRenderer>();
            controller = GetComponent<Controller2D>();
            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
            print("Gravity: " + gravity + "  Jump Velocity: " + maxJumpVelocity);

            plName.text = PhotonNetworkManager.playerName;
            //plName.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        if (!photonView.isMine)
        {
            plName.text = photonView.owner.NickName;
            //plName.color = photonView.GetComponent<Color>();
        }
    }

    // Update is called once per frame
    void Update () {

        playerAmount = GameObject.FindGameObjectsWithTag("Player").Length;
        //print(playerAmount);

        if (photonView.isMine)
        {
            CheckInput();
            if (loopOnce)
            {
                photonView.RPC("changeSprite", PhotonTargets.AllBuffered, playerAmount);
                loopOnce = false;
            }
        }
        else
            SmoothNetMovement();

	}

    [PunRPC]
    void changeSprite(int amount)
    {
        if (amount == 1)
        {
            GameHandler.Instance.char1 = true;
            characterModel.sprite = char1.sprite;
            characterModel.enabled = true;
        }
        if (amount == 2)
        {
            GameHandler.Instance.char2 = true;
            characterModel.sprite = char2.sprite;
            characterModel.enabled = true;
        }
        if (amount == 3)
        {
            GameHandler.Instance.char3 = true;
            characterModel.sprite = char3.sprite;
            characterModel.enabled = true;
        }
        if (amount == 4)
        {
            GameHandler.Instance.char4 = true;
            characterModel.sprite = char4.sprite;
            characterModel.enabled = true;
        }
    }

    public virtual void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        Debug.Log("Player Connected " + player.NickName);
    }


    public virtual void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        Debug.Log("Player Disconnected " + player.NickName);
    }

    [PunRPC]
    public void SetColor()
    {
        plName.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    private void CheckInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        int wallDirX = (controller.collisions.left) ? -1 : 1;

        playerScreenPoint = Camera.main.WorldToScreenPoint(mainPlayer.transform.position);

        if (Input.mousePosition.x > playerScreenPoint.x)
        {
            characterModel.flipX = false;
            gunModel.flipY = false;
            photonView.RPC("OnSpriteFlipFalse", PhotonTargets.Others);
        }
        else
        {
            characterModel.flipX = true;
            gunModel.flipY = true;
            photonView.RPC("OnSpriteFlipTrue", PhotonTargets.Others);
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        bool wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (input.x != wallDirX && input.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wallSliding)
            {
                if (wallDirX == input.x)
                {
                    velocity.x = -wallDirX * wallJumpClimb.x;
                    velocity.y = wallJumpClimb.y;
                }
                else if (input.x == 0)
                {
                    velocity.x = -wallDirX * wallJumpOff.x;
                    velocity.y = wallJumpOff.y;
                }
                else
                {
                    velocity.x = -wallDirX * wallLeap.x;
                    velocity.y = wallLeap.y;
                }
            }
            if (controller.collisions.below)
            {
                velocity.y = maxJumpVelocity;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shooting();
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime, input);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
    }

    private void Shooting()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 myPos = new Vector2(gunModel.transform.position.x, gunModel.transform.position.y);
        Vector2 direction = target - myPos;
        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        if (characterModel.flipX == false)
        {
            //GameObject obj = PhotonNetworkManager.Instantiate(bulletObject.name, new Vector2(aimPos.x, aimPos.y), Quaternion.identity, 0);

            GameObject projectile = PhotonNetworkManager.Instantiate(bulletObject.name, myPos, rotation, 0);

            //projectile.GetComponent<Rigidbody2D>().velocity = direction * 5f;
        }
        else
        {
            //GameObject obj = PhotonNetworkManager.Instantiate(bulletObject.name, new Vector2(aimPos.x, aimPos.y), Quaternion.identity, 0);

            GameObject projectile = PhotonNetworkManager.Instantiate(bulletObject.name, myPos, rotation, 0);

            //projectile.GetComponent<Rigidbody2D>().velocity = direction * 5f;
            //projectile.GetComponent<PhotonView>().RPC("ChangeDirection_Left", PhotonTargets.AllBuffered);
        }
        
    }

    private void SmoothNetMovement()
    {
        transform.position = Vector3.Lerp(transform.position, selfpos, Time.deltaTime * 8);
        transform.position = Vector3.Lerp(transform.position, velocity, Time.deltaTime * 8);
    }

    [PunRPC]
    private void OnSpriteFlipFalse()
    {
        characterModel.flipX = false;
        gunModel.flipY = false;
   
    }

    [PunRPC]
    private void OnSpriteFlipTrue()
    {
        characterModel.flipX = true;
        gunModel.flipY = true;
    }


    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.position);
        }
        else
        {
            selfpos = (Vector3)stream.ReceiveNext();
            velocity = (Vector3)stream.ReceiveNext();
        }
    }
}
