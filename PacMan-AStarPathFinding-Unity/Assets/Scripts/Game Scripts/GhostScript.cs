using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GhostScript : MonoBehaviour {

    private GameManager _gm;

    [HideInInspector]
    public static bool playerDead;

	void Start()
	{
	    _gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "pacman")
		{
            if (playerDead == false)
            {
                playerDead = true;
                _gm.LoseLife();
            }
		}
	}

}
