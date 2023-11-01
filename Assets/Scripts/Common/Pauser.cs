using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class Pauser : MonoBehaviour
    {
        public event UnityAction<bool> EventOnPauseStateChange;

        private bool isPause;
        public bool IsPause => isPause;

        public void ChangePauseState()
        {
            if (isPause) Unpause();
            else Pause();
        }

        public void Pause()
        {
            if (isPause) return;

            Time.timeScale = 0f;
            isPause = true;
            EventOnPauseStateChange?.Invoke(isPause);
        }

        public void Unpause()
        {
            if (!isPause) return;

            Time.timeScale = 1f;
            isPause = false;
            EventOnPauseStateChange?.Invoke(isPause);
        }

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Unpause();
        }
    }
}
