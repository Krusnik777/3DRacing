using UnityEngine;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class SaveResetter : MonoBehaviour
    {
        public void ResetSaves()
        {
            FileHandler.Reset(GameCompletion.Filename);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
