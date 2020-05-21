using UnityEngine;

namespace CG.Core
{
    public class Destructable : MonoBehaviour
    {
        public Health health;

        private void Start()
        {
            health.onDeathCallback += Death;
        }

        private void Death()
        {
            Destroy(gameObject);
        }
    }
}
