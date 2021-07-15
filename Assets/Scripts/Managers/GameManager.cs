using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas menuCanvas;
    public Canvas restartCanvas;

    public bool isStarted;
    public bool canShoot;

    public static GameManager instance;

    public ObjectivesManager objectivesManager;

    //LevelManagement  Variables
    public LevelManager levelManager;
    public Level currentLevel = null;

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
        restartCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (objectivesManager.ObjectivesComplete)
        {
            ShowRestartButton();
        }
    }

    public void StartTheApp()
    {
        isStarted = true;
        canShoot = true;

        menuCanvas.gameObject.SetActive(false);

        if (currentLevel == null) currentLevel = levelManager.Levels[0];

        levelManager.StartLevel(currentLevel);

        Grid.Instance.BlocksAreSpawned = true;
    }

    public void ShowRestartButton()
    {
        restartCanvas.gameObject.SetActive(true);
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        canShoot = false;
    }
}
