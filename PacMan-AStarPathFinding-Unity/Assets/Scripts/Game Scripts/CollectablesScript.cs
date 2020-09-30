using UnityEngine;
using System.Collections;

public class CollectablesScript : MonoBehaviour {

    private GameManager gm;

    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "pacman")
		{
			GameManager.score += 1;
		    GameObject[] coins= GameObject.FindGameObjectsWithTag("coin");
            Destroy(gameObject);

		    if (coins.Length == 1)
		    {
		       FindObjectOfType<GameGUINavigation>().ReturnToMenuWin();
		    }
		}

		if(other.name == "pacman" && this.gameObject.name == "cherry1" ||
		 this.gameObject.name == "cherry2" || 
		 this.gameObject.name == "cherry3" ||
		this.gameObject.name == "cherry4")
		{
			FindObjectOfType<GameGUINavigation>().ReturnToMenuWin();
		}
    }
}
