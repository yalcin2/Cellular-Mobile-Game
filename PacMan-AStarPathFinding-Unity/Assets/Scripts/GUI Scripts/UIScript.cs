using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour {

	public List<Image> lives = new List<Image>(4);

    Text txt_score, txt_timer;

    [HideInInspector]
    public int score;
    public static bool _isRunning;
    public static float _elapsedSeconds = 600.0f;

    private float minutes;
    private float seconds;

    public Text text;

    // Use this for initialization
    void Start () 
	{
        txt_score = GetComponentsInChildren<Text>()[0];
		txt_timer = GetComponentsInChildren<Text>()[1];

        _isRunning = true;

        StartCoroutine(updateCoroutine());

        for (int i = 0; i < 3 - GameManager.lives; i++)
	    {
	        Destroy(lives[lives.Count-1]);
            lives.RemoveAt(lives.Count-1);
	    }
        GhostScript.playerDead = false;
    }
	
	// Update is called once per frame
	void Update () 
	{
        score = GameManager.score;
		txt_score.text = "Score\n" + score;

        if (!_isRunning) return;

        _elapsedSeconds -= Time.deltaTime;

        minutes = Mathf.Floor(_elapsedSeconds / 60);
        seconds = _elapsedSeconds % 60;
        if (seconds > 59) seconds = 59;
        if (minutes < 0)
        {
            _isRunning = false;
            minutes = 0;
            seconds = 0;
            FindObjectOfType<GameGUINavigation>().ReturnToMenuFail();
        }
    }

    private IEnumerator updateCoroutine()
    {
        while (_isRunning)
        {
            txt_timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
