using System;
using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class GameGUINavigation : MonoBehaviour {

	private bool _paused;
    private bool quit;

	public float initialDelay;

	public Canvas PauseCanvas;
	public Canvas FinishedCanvas;
    public Canvas GameOverCanvas;
    public Text FinishedText;
	
	public Button MenuButton;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine("ShowReadyScreen", initialDelay);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			// if scores are show, go back to main menu
			if(GameManager.gameState == GameManager.GameState.Scores)
				Menu();

			// if in game, toggle pause or quit dialogue
			else
			{
				TogglePause();
			}
		}
	}

	public void H_ShowReadyScreen()
	{
		StartCoroutine("ShowReadyScreen", initialDelay);
	}

    public void ReturnToMenuFail()
    {
        GameOverCanvas.enabled = true;
        Time.timeScale = 0.0f;
    }

    public void ReturnToMenuWin()
    {
        
        FinishedCanvas.enabled = true;
        UIScript._isRunning = false;
        int time = Mathf.RoundToInt(UIScript._elapsedSeconds);
        int score = GameManager.score;
        FinishedText.text = "FINISHED, time taken is: " + time.ToString() +
                            ", total score is: " + score;
        Time.timeScale = 0.0f;
        UIScript._elapsedSeconds = 0;
    }

    IEnumerator ShowReadyScreen(float seconds)
	{
		GameManager.gameState = GameManager.GameState.Init;
		yield return new WaitForSeconds(seconds);
		GameManager.gameState = GameManager.GameState.Game;
	}

	public void TogglePause()
	{
		// if paused before key stroke, unpause the game
		if(_paused)
		{
			Time.timeScale = 1;
			PauseCanvas.enabled = false;
			_paused = false;
			MenuButton.enabled = true;
		}
		
		// if not paused before key stroke, pause the game
		else
		{
			PauseCanvas.enabled = true;
			Time.timeScale = 0.0f;
			_paused = true;
			MenuButton.enabled = false;
		}
	}

	public void Menu()
	{
		Application.LoadLevel("menu");
		Time.timeScale = 1.0f;
        UIScript._elapsedSeconds = 0;
        // take care of game manager
        GameManager.DestroySelf();
	}

    public void LoadLevel()
    {
        GameManager.Level++;
        UIScript._elapsedSeconds = 0;
        Application.LoadLevel("game");
    }
}
