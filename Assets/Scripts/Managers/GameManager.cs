using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas menuCanvas;
    public Button restartButton;
    public Button nextLevelButton;

    public bool isStarted;
    public bool canShoot;

    public static GameManager instance;

    public ObjectivesManager objectivesManager;

    //LevelManagement  Variables
    public LevelManager levelManager;
    public int currentLevel = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        isStarted = false;
        canShoot = false;
        restartButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (objectivesManager.ObjectivesComplete)
        {
            // ShowRestartButton();
            ShowNextLevelButton();
        }
    }

    public void StartTheApp()
    {
        isStarted = true;
        canShoot = true;

        menuCanvas.gameObject.SetActive(false);

        levelManager.StartLevel(currentLevel);

        Grid.Instance.BlocksAreSpawned = true;
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

    public void NextLevel()
    {
        levelManager.NextLevel();
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        canShoot = false;
    }
}
