using UnityEngine;

namespace CG.Core
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        new public string name = "New Item";
        public Sprite icon = null;
        public int stacks = 1;
        public int amount = 1;

        public Item(Item item)
        {
            name = item.name;
            icon = item.icon;
            stacks = item.stacks;
            amount = item.amount;
        }

        public void CopyItemAttributes(Item newItem)
        {
            newItem.name = name;
            newItem.icon = icon;
            newItem.stacks = stacks;
            newItem.amount = amount;
        }

        public void Use()
        {
            Inventory.instance.UseItem(this);
        }

        public void Split()
        {
            Inventory.instance.SplitItem(this);
        }
    }
}
