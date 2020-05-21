using UnityEngine;
using CG.Core;
using UnityEngine.UI;

namespace CG.Control
{
    public class EndGame : MonoBehaviour
    {
        public Health playerHealth;
        public Health[] enemiesHealth;
        public CameraMovement cameraMovement;
        public Text endGameText;

        public int pickupsInLevel;
        int pickedPickups;
        int enemyKills;
        bool isComplete;

        private void Start()
        {
            isComplete = false;
            enemyKills = 0;
            pickedPickups = 0;
            playerHealth.onDeathCallback += Die;
            cameraMovement.onItemPickedUpCallback += NewItem;
            foreach (Health enemyHealth in enemiesHealth)
            {
                enemyHealth.onDeathCallback += EnemyKilled;
            }
        }

        private void Update()
        {
            int wantedNumber = pickupsInLevel + enemiesHealth.Length;
            int currentNumber = pickedPickups + enemyKills;
            if (currentNumber < wantedNumber)
            {
                float percentage = (float)currentNumber / (float)wantedNumber;
                percentage *= 100f;
                endGameText.text = $"Completed: {percentage.ToString("0.00")}%";
            } else
            {
                isComplete = true;
                endGameText.text = "Step in to end the game";
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && isComplete)
            {
                Core.Core.instance.EndGame(true);
            }
        }

        private void EnemyKilled()
        {
            enemyKills++;
        }

        private void NewItem()
        {
            pickedPickups++;
        }

        private void Die()
        {
            Core.Core.instance.EndGame(false);
        }
    }
}