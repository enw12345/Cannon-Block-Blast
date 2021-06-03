using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas spaceStartCanvas;
    public Canvas restartCanvas;

    public bool isStarted;

    public static GameManager instance;
    [SerializeField] private BulletTypeContainer PlayersBullets;

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
        ResetPlayerBullets();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            StartTheApp();
        }
    }

    public void StartTheApp()
    {
        isStarted = true;

        spaceStartCanvas.gameObject.SetActive(false);
    }

    public void ShowRestartButton()
    {
        restartCanvas.gameObject.SetActive(true);
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        ResetPlayerBullets();
    }

    private void ResetPlayerBullets()
    {
        foreach (BulletType playerBulletType in PlayersBullets.Container)
        {
            playerBulletType.AmmoCount = playerBulletType.DefaultAmmoCount;
        }
    }

}
