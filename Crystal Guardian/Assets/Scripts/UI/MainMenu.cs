using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CG.UI
{
    public class MainMenu : MonoBehaviour
    {
        // Called by UI trigger
        public void PlayGame()
        {
            StartCoroutine(Transition());
        }

        // Called by UI trigger
        public void ExitGame()
        {
            Debug.Log("QUIT!");
            Application.Quit();
        }

        public IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(1);
            Destroy(gameObject);
        }
    }
}
