using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatScript : MonoBehaviour
{

    //public GameObject background;

    private int colorMode;

    private Color normalMode;
    private Color switchingMode_1;
    private Color switchingMode_2;
    private Color switchingMode_3;
    private Color alertMode;

    public bool testing_;

    // Start is called before the first frame update
    void Start()
    {
        //normalMode = new Color32(48, 48, 48, 255);
        //switchingMode_1 = new Color32(46, 48, 45, 255);
        //switchingMode_2 = new Color32(44, 48, 42, 255);
        //switchingMode_3 = new Color32(42, 48, 40, 255);
        //alertMode = new Color32(38, 48, 38, 255);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!testing_)
        {
            if (col.gameObject.tag == "Consumable")
            {
                //StopAllCoroutines();
                //StartCoroutine(BackgroundColourNotifer());
                ValuesScript.coinScore++;
                ValuesScript.playerExp = ValuesScript.playerExp + ValuesScript.expToRank;
                ValuesScript.availableCoins--;
                Destroy(col.gameObject);
            }
            else
            {
                Debug.Log("Collided with: " + col.gameObject.name);
            }
        }
   }

    /*
    private IEnumerator BackgroundColourNotifer()
    {
        background.GetComponent<SpriteRenderer>().color = switchingMode_1;
        yield return new WaitForSeconds(0.05f);
        background.GetComponent<SpriteRenderer>().color = switchingMode_2;
        yield return new WaitForSeconds(0.05f);
        background.GetComponent<SpriteRenderer>().color = switchingMode_3;
        yield return new WaitForSeconds(0.05f);
        background.GetComponent<SpriteRenderer>().color = alertMode;
        yield return new WaitForSeconds(0.05f);
        background.GetComponent<SpriteRenderer>().color = switchingMode_3;
        yield return new WaitForSeconds(0.05f);
        background.GetComponent<SpriteRenderer>().color = switchingMode_2;
        yield return new WaitForSeconds(0.05f);
        background.GetComponent<SpriteRenderer>().color = switchingMode_1;
        yield return new WaitForSeconds(0.05f);
        background.GetComponent<SpriteRenderer>().color = normalMode;
    }
    */
}
