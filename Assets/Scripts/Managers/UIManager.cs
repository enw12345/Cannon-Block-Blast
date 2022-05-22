using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public enum CanvasType
    {
        StartMenuCanvas,
        GameplayCanvas,
        RestartCanvas,
        ContinueCanvas
    }

    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [Header("Buttons")] public Button nextLevelButton;

        public Button restartButton;
        private List<CanvasController> canvasControllers;
        private CanvasController lastActiveCanvas;

        private void Awake()
        {
            Instance = this;
            canvasControllers = FindObjectsOfType<CanvasController>().ToList();
            canvasControllers.ForEach(x => x.gameObject.SetActive(false));

            SwitchCanvas(CanvasType.StartMenuCanvas);
        }

        public void SwitchCanvas(CanvasType canvasType)
        {
            if (lastActiveCanvas != null) lastActiveCanvas.TurnOffCanvas();

            var newActiveCanvas = canvasControllers.Find(x => x.canvasType == canvasType);
            if (!VerifyCanvas(newActiveCanvas)) return;
            newActiveCanvas.TurnOnCanvas();
            lastActiveCanvas = newActiveCanvas;
        }

        private static bool VerifyCanvas(Object canvasController)
        {
            if (canvasController != null)
            {
                return true;
            }

            Debug.LogWarning("Canvas not found.");
            return false;
        }
    }
}