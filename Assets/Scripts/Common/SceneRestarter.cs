using UnityEngine;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class SceneRestarter : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

            if (Input.GetKeyDown(KeyCode.F8))
            {
                PlayerPrefs.DeleteAll();
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.Tab)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            if (Input.GetKeyDown(KeyCode.F5))
            {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}