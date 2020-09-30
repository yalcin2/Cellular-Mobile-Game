using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject playerObject;
    Vector2 direction;
    float timeStamp;
    bool flyToCat;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (flyToCat)
        {
            direction = -(transform.position - playerObject.transform.position).normalized;
            rb.velocity = new Vector2(direction.x, direction.y) * (ValuesScript.magnetPower * 2) * (Time.time / timeStamp);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (ValuesScript.magnetUnlocked == "yes")
        {
            if (col.gameObject.tag == ("PlayerMagnet"))
            {
                timeStamp = Time.time;
                playerObject = GameObject.FindGameObjectWithTag("Player");
                flyToCat = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (ValuesScript.magnetUnlocked == "yes")
        {
            if (col.gameObject.tag == ("PlayerMagnet"))
            {
                rb.velocity = Vector3.zero;
                flyToCat = false;
            }
        }
    }
}
