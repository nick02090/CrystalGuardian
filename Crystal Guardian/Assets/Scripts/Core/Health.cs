using UnityEngine;

namespace CG.Core
{
    public class Health : MonoBehaviour
    {
        public int maxHealth = 100;
        public HealthBar healthBar;

        public delegate void OnDeath();
        public OnDeath onDeathCallback;

        int health;
        private void Start()
        {
            health = maxHealth;
            healthBar.SetMaxHealth(health);
        }

        public void TakeDamage(int damage)
        {
            health = Mathf.Clamp(health - damage, 0, maxHealth);
            healthBar.SetHealth(health);
            if (health == 0)
                TriggerDeathUpdate();
        }

        public void Heal(int healAmount)
        {
            health = Mathf.Clamp(health + healAmount, 0, maxHealth);
            healthBar.SetHealth(health);
        }

        public int GetHealth()
        {
            return health;
        }

        private void TriggerDeathUpdate()
        {
            if (onDeathCallback != null)
                onDeathCallback.Invoke();
        }
    }
}
