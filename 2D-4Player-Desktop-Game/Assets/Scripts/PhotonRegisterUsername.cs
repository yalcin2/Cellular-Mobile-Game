using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonRegisterUsername : MonoBehaviour {

    public InputField usernameInput;

    public Button createButton;

    void Update()
    {
        UsernameInputChange();
    }

    public void UsernameInputChange()
    {
        if (usernameInput.text.Length >= 2)
            createButton.interactable = true;
        else
            createButton.interactable = false;

    }

    public void CreateUsername()
    {
        PhotonNetworkManager.playerName = usernameInput.text + " (" + Random.Range(1, 10000) + ")";
        Debug.Log("Player`s name is set to: " + PhotonNetworkManager.playerName);
    }
}
