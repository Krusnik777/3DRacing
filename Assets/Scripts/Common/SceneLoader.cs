using UnityEngine;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class SceneLoader : MonoBehaviour
    {
        private const string MainMenuSceneName = "MainMenu";
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(MainMenuSceneName);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
