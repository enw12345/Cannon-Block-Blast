using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Canvas StartMenuCanvas;
    public Button nextLevelButton;
    public Button restartButton;

    private void Awake()
    {
        instance = this;
        restartButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
    }

    public void ShowRestartButton()
    {
        restartButton.gameObject.SetActive(true);
    }

    public void ShowNextLevelButton()
    {
        nextLevelButton.gameObject.SetActive(true);
    }

    public void HideNextLevelButton()
    {
        nextLevelButton.gameObject.SetActive(false);
    }

    public void HideStartMenu()
    {
        StartMenuCanvas.gameObject.SetActive(false);
    }
}
