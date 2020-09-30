using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //--------------------------------------------------------
    // Game variables

    public static int Level = 0;
    public static int lives = 4;

	public enum GameState { Init, Game, Dead, Scores }
	public static GameState gameState;

    public GameObject pacman;
    public GameObject blinky;
    public GameObject pinky;
    public GameObject inky;
    public GameObject clyde;

    public GameGUINavigation gui;

    public AIPath blinkyAI;
    public AIPath pinkyAI;
    public AIPath inkyAI;
    public AIPath clydeAI;

    static public int score;

    public float SpeedPerLevel;
    
    //-------------------------------------------------------------------
    // singleton implementation
    private static GameManager _instance;

    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if(this != _instance)   
                Destroy(this.gameObject);
        }
    }

	void Start () 
	{
		gameState = GameState.Init;
    }

    void OnLevelWasLoaded()
    {

        lives = 4;


        SetSpeed();

        Debug.Log("Level " + Level + " Loaded! with " + lives + "lives");

        pacman.GetComponent<PlayerController>().speed += Level*SpeedPerLevel/2;
    }


    // Update is called once per frame
	void Update () { }

	public void ResetScene()
	{
        pacman.transform.position = new Vector3(15f, 11f, 0f);
		blinky.transform.position = new Vector3(15f, 20f, 0f);
		pinky.transform.position = new Vector3(14.5f, 17f, 0f);
		inky.transform.position = new Vector3(16.5f, 17f, 0f);
		clyde.transform.position = new Vector3(12.5f, 17f, 0f);

        blinkyAI.maxSpeed = 0;
        pinkyAI.maxSpeed = 0;
        inkyAI.maxSpeed = 0;
        clydeAI.maxSpeed = 0;

        pacman.GetComponent<PlayerController>().ResetDestination();

        gameState = GameState.Init;  
        gui.H_ShowReadyScreen();

        StartCoroutine("WaitForMovement");

    }

    void SetSpeed()
    {
        blinkyAI.maxSpeed = Level + 0.5f;
        pinkyAI.maxSpeed = Level + 1;
        inkyAI.maxSpeed = Level + 1.5f;
        clydeAI.maxSpeed = Level + 2;
    }

    public void LoseLife()
    {
        lives--;
        gameState = GameState.Dead;
        StartCoroutine("WaitForRespawn");

        // update UI too
        UIScript ui = GameObject.FindObjectOfType<UIScript>();
        Destroy(ui.lives[ui.lives.Count - 1]);
        ui.lives.RemoveAt(ui.lives.Count - 1);


    }

    IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(2f);
        GhostScript.playerDead = false;
    }

    IEnumerator WaitForMovement()
    {
        yield return new WaitForSeconds(2f);
        SetSpeed();
    }


    public static void DestroySelf()
    {
        score = 0;
        Level = 0;
        lives = 4;
        Destroy(GameObject.Find("Game Manager"));
    }
}
