using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public Canvas menuCanvas;

    public bool isStarted;
    public bool canShoot;

    public static GameManager instance;

    public ObjectivesManager objectivesManager;

    [Header("Level Manager Variables")]
    public LevelManager levelManager;
    public int startLevel = 0;

    public EventHandler resetEvent;

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
            UIManager.instance.ShowContinueButton();
        }
    }

    public void StartTheApp()
    {
        isStarted = true;
        canShoot = true;

        UIManager.instance.SwitchCanvas(CanvasType.GameplayCanvas);

        levelManager.StartLevel(startLevel);

        Grid.Instance.BlocksAreSpawned = true;
    }

    public void NextLevel()
    {
        levelManager.NextLevel();
    }

    // public void Restart()
    // {
    //     Scene scene = SceneManager.GetActiveScene();
    //     SceneManager.LoadScene(scene.name);
    //     canShoot = false;
    // }

    public void Restart()
    {
        levelManager.ResetLevel();
        resetEvent?.Invoke(this, EventArgs.Empty);
    }
}
