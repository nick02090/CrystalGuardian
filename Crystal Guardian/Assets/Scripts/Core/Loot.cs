using UnityEngine;

namespace CG.Core
{
    public class Loot : MonoBehaviour
    {
        public Item lootItem;
        public Health health;

        private void Start()
        {
            health.onDeathCallback += AddLoot;
        }

        private void AddLoot()
        {
            Item newItem = ScriptableObject.CreateInstance<Item>();
            lootItem.CopyItemAttributes(newItem);
            Inventory.instance.AddItem(newItem);
        }
    }
}
