using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour
{
    
    public float moveSpeed = 1f;
    public JoyStickScript jsMovement;

    private Vector3 direction;
    private float xMin, xMax, yMin, yMax;

    void Update()
    {

        direction = jsMovement.InputDirection; //InputDirection can be used as per the need of your project

        if (direction.magnitude != 0)
        {

            transform.position += direction;
            //transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), Mathf.Clamp(transform.position.y, yMin, yMax), 0f);//to restric movement of player
        }
    }

    void Start()
    {

        //Initialization of boundaries
        xMax = Screen.width - 50; // I used 50 because the size of player is 100*100
        xMin = 50;
        yMax = Screen.height - 50;
        yMin = 50;
    }

    /*
    public bool testing_;

    private Vector3 targetPosition;

    private Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayerToInputDevice();
    }

    private void movePlayerToInputDevice()
    {
        if (Input.touchCount > 0)
        {
            // The screen has been touched so store the touch
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                // If the finger is on the screen, move the object smoothly to the touch position
                targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
            }
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime); //* ValuesScript.playerSpeedMultiplier);
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Border" || col.gameObject.tag == "Player")
        {
            targetPosition = this.transform.position;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Border" || col.gameObject.tag == "Player")
        {
            targetPosition = this.transform.position;
        }
    }

    public void shopButton() {

        targetPosition.x = this.transform.position.x;
        targetPosition.y = this.transform.position.y;

    }
    */

}
