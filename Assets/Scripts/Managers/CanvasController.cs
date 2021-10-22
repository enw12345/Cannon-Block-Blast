using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public CanvasType canvasType;

    public void TurnOnCanvas()
    {
        gameObject.SetActive(true);
    }

    public void TurnOffCanvas()
    {
        gameObject.SetActive(false);
    }
}
