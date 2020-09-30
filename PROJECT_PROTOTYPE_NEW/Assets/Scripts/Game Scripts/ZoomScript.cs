using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomScript : MonoBehaviour
{
    private bool zoomed;

    public Button zoomBtn;

    void Start()
    {
        zoomed = false;
        zoomBtn.interactable = true;
    }

    void Update()
    {

    }

    public void onZoomBtnPressed()
    {
        if (!zoomed)
        {
            zoomBtn.interactable = false;
            StartCoroutine(returnBackToNormalZoom());
            Camera.main.orthographicSize = 7f;
            zoomed = true;
        }
        else
        {
            zoomBtn.interactable = true;
            Camera.main.orthographicSize = 4f;
            zoomed = false;
        }
    }

    private IEnumerator returnBackToNormalZoom()
    {
        yield return new WaitForSeconds(5f);
        Camera.main.orthographicSize = 4f;
        zoomed = false;
        zoomBtn.interactable = true;
    }
}
