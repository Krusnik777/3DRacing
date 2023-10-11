using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (Input.GetKeyDown(KeyCode.Tab)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
