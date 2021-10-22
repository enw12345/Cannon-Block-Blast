using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public Canvas menuCanvas;

    public bool isStarted;
    public bool canShoot;

    public static GameManager instance;

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

    public void StartTheApp()
    {
        isStarted = true;
        canShoot = true;

        UIManager.instance.SwitchCanvas(CanvasType.GameplayCanvas);

        levelManager.SelectLevel(startLevel);
    }

    public void NextLevel()
    {
        levelManager.NextLevel();
    }

    public void Restart()
    {
        levelManager.ResetLevel();
        resetEvent?.Invoke(this, EventArgs.Empty);
    }
}
