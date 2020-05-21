using UnityEngine;
using CG.Core;

namespace CG.Control
{
    public class Bullet : MonoBehaviour
    {
        public float waitForSeconds = 2.0f;
        public float speed = 8f;

        private void Update()
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Health enemyHealth = other.GetComponent<Health>();
                enemyHealth.TakeDamage(20);
                Invoke("OnDisable", 0f);
            }
            if (other.CompareTag("Mine"))
            {
                Health mineHealth = other.GetComponent<Health>();
                mineHealth.TakeDamage(40);
                Invoke("OnDisable", 0f);
            }
        }

        private void OnEnable()
        {
            Invoke("OnDisable", waitForSeconds);
        }

        private void OnDisable()
        {
            transform.gameObject.SetActive(false);
        }
    }
}
