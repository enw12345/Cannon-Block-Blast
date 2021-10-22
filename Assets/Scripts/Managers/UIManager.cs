using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum CanvasType
{
    StartMenuCanvas,
    GameplayCanvas,
    RestartCanvas,
    ContinueCanvas
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private List<CanvasController> CanvasControllers;
    private CanvasController lastActiveCanvas;

    [Header("Buttons")]
    public Button nextLevelButton;
    public Button restartButton;

    private void Awake()
    {
        instance = this;
        CanvasControllers = FindObjectsOfType<CanvasController>().ToList();
        CanvasControllers.ForEach(x => x.gameObject.SetActive(false));

        SwitchCanvas(CanvasType.StartMenuCanvas);
    }

    public void SwitchCanvas(CanvasType canvasType)
    {
        if (lastActiveCanvas != null)
        {
            lastActiveCanvas.TurnOffCanvas();
        }

        CanvasController newActiveCanvas = CanvasControllers.Find(x => x.canvasType == canvasType);
        if (VerifyCanvas(newActiveCanvas))
        {
            newActiveCanvas.TurnOnCanvas();
            lastActiveCanvas = newActiveCanvas;
        }
    }

    private bool VerifyCanvas(CanvasController canvasController)
    {
        if (canvasController != null)
        {
            return true;
        }
        else
        {
            Debug.LogWarning("Canvas not found.");
            return false;
        }
    }
}
