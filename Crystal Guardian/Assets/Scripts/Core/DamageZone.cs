using System.Collections;
using UnityEngine;

namespace CG.Core
{
    public class DamageZone : MonoBehaviour
    {
        public ParticleSystem playerParticleSystem;
        public GameObject player;
        public int damageAmount = 10;
        public float damageTime = 0.5f;

        bool doDamge = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player)
            {
                doDamge = true;
                playerParticleSystem.Play();
                StartCoroutine(DamagePlayer());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == player)
            {
                doDamge = false;
                playerParticleSystem.Stop();
                StopCoroutine(DamagePlayer());
            }
        }

        private IEnumerator DamagePlayer()
        {
            Health playerHealth = player.GetComponent<Health>();
            while (doDamge)
            {
                playerHealth.TakeDamage(damageAmount);
                yield return new WaitForSeconds(damageTime);
            }
        }
    }
}
