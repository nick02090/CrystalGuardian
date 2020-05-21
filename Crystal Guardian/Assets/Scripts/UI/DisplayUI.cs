using UnityEngine;
using UnityEngine.UI;

namespace CG.UI
{
    public class DisplayUI : MonoBehaviour
    {
        public string displayText;
        public Text text;
        public Color color;
        public float fadeTime;

        bool isVisible;

        private void Start()
        {
            text.color = Color.clear;
        }

        private void Update()
        {
            FadeText();
        }

        public void SetTextState(bool state)
        {
            isVisible = state;
        }

        private void FadeText()
        {
            if (isVisible)
            {
                transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
                text.text = displayText;
                text.color = Color.Lerp(text.color, color, fadeTime * Time.deltaTime);
            }
            else
            {
                text.color = Color.Lerp(text.color, Color.clear, fadeTime * Time.deltaTime);
            }
        }
    }
}
