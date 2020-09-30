using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{

    private int currentLevel;

    private float currentSize;
    private int currentMagnetSize;

    public GameObject playerObject;

    public static bool sizeIncreased;
    public static bool magnetRangeIncreased;

    public GameObject magnetField;

    public GameObject background;

    private Color normalMode;
    private Color alertMode;



    // Start is called before the first frame update
    void Start()
    {
        normalMode = new Color32(40, 50, 63, 255);
        alertMode = new Color32(126, 94, 25, 255);

        sizeIncreased = false;
        magnetRangeIncreased = false;

        currentSize = ValuesScript.playerSize;
        currentMagnetSize = ValuesScript.magnetRange;
        if (currentSize == 0)
        {
            playerObject.transform.localScale = new Vector3(1, 1, 0);
        }
        else
        {
            playerObject.transform.localScale = new Vector3(currentSize, currentSize, 0);
        }

        if (currentMagnetSize == 1)
        {
            magnetField.transform.localScale = new Vector3(5f, 5f, 0);
        }
        else if (currentMagnetSize == 2)
        {
            magnetField.transform.localScale = new Vector3(6f, 6f, 0);
        }
        else if (currentMagnetSize == 3)
        {
            magnetField.transform.localScale = new Vector3(7f, 7f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        increaseLevelIntensity();
        increasePlayerSize();
    }

    private IEnumerator BackgroundColourNotifer()
    {
        background.GetComponent<SpriteRenderer>().color = alertMode;
        yield return new WaitForSeconds(0.5f);
        background.GetComponent<SpriteRenderer>().color = normalMode;
    }

    private void increaseLevelIntensity()
    {
        currentLevel = ValuesScript.currentLevel;

        if (currentLevel <= 5)
        {
            ValuesScript.expToRank = 0.03f;
        }
        else if (currentLevel > 5 && currentLevel <= 15)
        {
            ValuesScript.expToRank = 0.007f;
        }
        else if (currentLevel > 15 && currentLevel <= 25)
        {
            ValuesScript.expToRank = 0.003f;
        }
        else if (currentLevel > 25 && currentLevel <= 50)
        {
            ValuesScript.expToRank = 0.001f;
        }
        else if (currentLevel > 50 && currentLevel <= 100)
        {
            ValuesScript.expToRank = 0.00001f;
        }
    }

    public void increasePlayerSize()
    {
        if (sizeIncreased) {
            if (ValuesScript.playerSize <= 2.5)
            {
                playerObject.transform.localScale += new Vector3(0.05f, 0.05f, 0);
                currentSize = currentSize + 0.05f;
                ValuesScript.playerSize = currentSize;
                sizeIncreased = false;
                StartCoroutine(BackgroundColourNotifer());
            }
            else {
                print("MAX SIZE HAS BEEN REACHED");
            }
        }
    }

    public void increaseMagnetRange()
    {
       if (magnetRangeIncreased)
        {
            if (ValuesScript.magnetRange == 2)
            {
                magnetField.transform.localScale = new Vector3(6f, 6f, 0);
                magnetRangeIncreased = false;
            }
            else if (ValuesScript.magnetRange == 3)
            {
                magnetField.transform.localScale = new Vector3(7f, 7f, 0);
                magnetRangeIncreased = false;
            }
        }
    }

}
