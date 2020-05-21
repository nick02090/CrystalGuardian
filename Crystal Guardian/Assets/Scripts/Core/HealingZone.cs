using System.Collections;
using UnityEngine;

namespace CG.Core
{
    public class HealingZone : MonoBehaviour
    {
        public ParticleSystem myParticleSystem;
        public GameObject player;
        public int healAmount = 10;
        public float healTime = 0.5f;

        bool doHealing = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player)
            {
                doHealing = true;
                myParticleSystem.Play();
                StartCoroutine(HealPlayer());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == player)
            {
                doHealing = false;
                myParticleSystem.Stop();
                StopCoroutine(HealPlayer());
            }
        }

        private IEnumerator HealPlayer()
        {
            Health playerHealth = player.GetComponent<Health>();
            while (doHealing)
            {
                playerHealth.Heal(healAmount);
                yield return new WaitForSeconds(healTime);
            }
        }
    }
}
