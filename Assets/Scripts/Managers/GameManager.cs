using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Canvas menuCanvas;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (objectivesManager.ObjectivesComplete)
        {
            UIManager.instance.ShowNextLevelButton();
        }
    }

    public void StartTheApp()
    {
        isStarted = true;
        canShoot = true;

        levelManager.StartLevel(currentLevel);

        Grid.Instance.BlocksAreSpawned = true;
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
