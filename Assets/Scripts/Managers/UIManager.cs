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
    public List<CanvasController> CanvasControllers;
    private CanvasController lastActiveCanvas;

    [Header("Buttons")]
    public Button nextLevelButton;
    public Button restartButton;

    [Header("Rect Transforms")]
    public RectTransform startButton;

    public float bouncePercent;

    private void Awake()
    {
        instance = this;
        CanvasControllers = FindObjectsOfType<CanvasController>().ToList();
        CanvasControllers.ForEach(x => x.gameObject.SetActive(false));

        SwitchCanvas(CanvasType.StartMenuCanvas);
    }

    private void OnEnable()
    {
        LeanTweenManager.BounceRect(startButton, bouncePercent, .5f);
    }

    public void ShowContinueButton()
    {
        nextLevelButton.gameObject.SetActive(true);
    }

    public void ShowRestartButton()
    {
        restartButton.gameObject.SetActive(true);
    }

    public void HideButtons()
    {
        nextLevelButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    public void SwitchCanvas(CanvasType canvasType)
    {
        if (lastActiveCanvas != null)
        {
            lastActiveCanvas.gameObject.SetActive(false);
        }

        CanvasController newActiveCanvas = CanvasControllers.Find(x => x.canvasType == canvasType);
        if (VerifyCanvas(newActiveCanvas))
        {
            newActiveCanvas.gameObject.SetActive(true);
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
