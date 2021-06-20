using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas spaceStartCanvas;
    public Canvas restartCanvas;

    public bool isStarted;

    public static GameManager instance;

    public ObjectivesManager objectivesManager;

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
        restartCanvas.gameObject.SetActive(false);
        //   ResetPlayerBullets();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarted && Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            StartTheApp();
        }
    }

    public void StartTheApp()
    {
        isStarted = true;

        spaceStartCanvas.gameObject.SetActive(false);

        objectivesManager.InitializeObjectives();

        StartCoroutine(Grid.Instance.CreateGridOfBlocksStep());

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
    }
}
