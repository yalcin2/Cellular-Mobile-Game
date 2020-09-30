using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHidePaddingScript : MonoBehaviour
{

    public GameObject topPanel;
    public GameObject targetPosition;
    public GameObject returnPosition;


    public Text showHideText;

    private bool hidden;

    // Start is called before the first frame update
    void Start()
    {
        hidden = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hidden)
        {
            topPanel.transform.position = Vector3.Lerp(topPanel.transform.position,
                                                       targetPosition.transform.position, Time.deltaTime * 5);
            showHideText.text = "▼";
        }
        else if (hidden)
        {
            topPanel.transform.position = Vector3.Lerp(topPanel.transform.position,
                                                       returnPosition.transform.position, Time.deltaTime * 5);
            showHideText.text = "▲";
        }
    }

    public void showHideBtn() {
        if (!hidden)
        {
            hidden = true;
        }
        else if (hidden) {
            hidden = false;
        }
    }


}
