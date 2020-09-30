using UnityEngine;
using System.Collections;

public class MenuNavigation : MonoBehaviour {

	public void MainMenu()
	{
		Application.LoadLevel("menu");
	}

	public void Quit()
	{
		Application.Quit();
	}
	
	public void Play()
	{
		Application.LoadLevel("game");
	}

    public void SetDifficultyEasyLevel()
    {
        GameManager.Level = 3;
    }

    public void SetDifficultyMediumLevel()
    {
        GameManager.Level = 4;
    }

    public void SetDifficultyHardLevel()
    {
        GameManager.Level = 7;
    }

}
