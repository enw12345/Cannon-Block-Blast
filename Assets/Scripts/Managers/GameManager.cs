using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public Canvas menuCanvas;

        [SerializeField] public bool isStarted;
        [SerializeField] public bool canShoot;

        [Header("Level Manager Variables")] public LevelManager levelManager;

        public int startLevel;

        public EventHandler resetEvent;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;

            isStarted = false;
            canShoot = false;
        }

        public void StartTheApp()
        {
            isStarted = true;
            canShoot = true;

            UIManager.Instance.SwitchCanvas(CanvasType.GameplayCanvas);

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
}