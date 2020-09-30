using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGameTextScript : MonoBehaviour
{
    public float letterPause = 0.2f;
    public AudioClip typeSound1;
    public AudioClip typeSound2;

    string message;
    public Text textComp;

    private int checkFinish;
    private bool playOnce;

    private bool messageFinished;

    // Use this for initialization
    void Start()
    {
        playOnce = true;
        messageFinished = false;

        if (ValuesScript.newPlayer == null)
        {
            textComp.text = "";
            message = "Greetings, PLAYER!";
        }
        else
        {
            textComp.text = "";
            message = "Welcome back!";
        }
    }

    private void Update()
    {
        if (playOnce) {
            StartCoroutine(TypeText());
            playOnce = false;
        }
    }

    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        { 
            textComp.text += letter;
            checkFinish++;

            if (checkFinish == message.Length)
            {
                yield return new WaitForSeconds(5f);
                textComp.text = "";
                checkFinish = 0;
            }
            if (typeSound1 && typeSound2)
                SoundManagerScript.instance.RandomizeSfx(typeSound1, typeSound2);
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }

}
