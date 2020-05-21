using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CG.Core
{
    public class Core : MonoBehaviour
    {
        public static Core instance;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if (instance != null)
                Destroy(gameObject);
            else
                instance = this;
        }

        private void Update()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (sceneIndex == 1)
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    StartCoroutine(NormalMainMenu());
                }
            }
        }

        public void EndGame(bool isWinner)
        {
            if (isWinner)
                StartCoroutine(WinnerMainMenu());
            else
                StartCoroutine(LoserMainMenu());
        }

        public IEnumerator NormalMainMenu()
        {
            yield return SceneManager.LoadSceneAsync(0);
            Cursor.visible = true;
        }

        public IEnumerator WinnerMainMenu()
        {
            yield return SceneManager.LoadSceneAsync(0);
            Cursor.visible = true;
            GameObject title = GameObject.FindGameObjectWithTag("Title");
            title.GetComponent<TextMeshProUGUI>().text = "You are victorious!";
            GameObject playButton = GameObject.FindGameObjectWithTag("PlayButton");
            playButton.GetComponent<TextMeshProUGUI>().text = "RESTART";
        }

        public IEnumerator LoserMainMenu()
        {
            yield return SceneManager.LoadSceneAsync(0);
            Cursor.visible = true;
            GameObject title = GameObject.FindGameObjectWithTag("Title");
            title.GetComponent<TextMeshProUGUI>().text = "You died!";
            GameObject playButton = GameObject.FindGameObjectWithTag("PlayButton");
            playButton.GetComponent<TextMeshProUGUI>().text = "RESTART";
        }
    }
}
