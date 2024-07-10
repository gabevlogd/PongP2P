namespace GV.UI
{
    using System.Collections.Generic;
    using UnityEngine;

    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Window _startingWindow;

        private Dictionary<Window, UIWindow> _windowsList;
        private UIWindow _currentWindow;
        private UIWindow _previousWindow;
        private static UIWindow _lastWindow;

        private void Awake() => InitializeManager();

        private void OnDisable() => _lastWindow = _currentWindow;

        private void InitializeManager()
        {
            _windowsList = new Dictionary<Window, UIWindow>()
            {
                { Window.MainMenu, GetComponentInChildren<MainMenu>(true) },
                { Window.PauseMenu, GetComponentInChildren<PauseMenu>(true) },
                { Window.HUD, GetComponentInChildren<HUD>(true) }
            };

            _currentWindow = GetStartingWindow();
            _currentWindow.OpenWindow();
        }

        private UIWindow GetStartingWindow()
        {
            return _startingWindow == Window.LastShowed ? _lastWindow : _windowsList[_startingWindow];
        }

        public void ChangeWindow(Window windowID)
        {
            _currentWindow.CloseWindow();
            _previousWindow = _currentWindow;
            _currentWindow = _windowsList[windowID];
            _currentWindow.OpenWindow();
        }
    }

    public enum Window
    {
        MainMenu,
        PauseMenu,
        HUD,
        LastShowed
    }
}
