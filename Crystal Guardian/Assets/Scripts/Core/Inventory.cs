using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CG.Core
{
    public class Inventory : MonoBehaviour
    {
        #region Singleton
        public static Inventory instance;
        private void Awake()
        {
            instance = this;
        }
        #endregion

        public delegate void OnItemChanged();
        public OnItemChanged onItemChangedCallback;
        public Transform player;

        public int maxSpace = 20;
        public bool isOpened = false;
        public List<Item> items = new List<Item>();

        public bool AddItem(Item item)
        {
            Item existingItem = items.FirstOrDefault(x => x.name.Equals(item.name) && x.amount < x.stacks);
            if (existingItem != null)
            {
                int sum = existingItem.amount + item.amount;
                if (sum <= existingItem.stacks)
                {
                    existingItem.amount += item.amount;
                    TriggerItemChangeUpdate();
                    return true;
                }
                else
                {
                    existingItem.amount = existingItem.stacks;
                    item.amount = sum - existingItem.amount;
                }
            }
            if (items.Count >= maxSpace)
            {
                Debug.Log("Inventory full!");
                return false;
            }
            items.Add(item);
            TriggerItemChangeUpdate();
            return true;
        }

        private void TriggerItemChangeUpdate()
        {
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
            TriggerItemChangeUpdate();
        }

        public void PutItemOn(int itemIndex, int newLocationIndex)
        {
            if (newLocationIndex < items.Count)
            {
                Item newItem = items[itemIndex];
                Item item = items[newLocationIndex];
                // if they are same type merge them
                if (newItem.name.Equals(item.name))
                {
                    int maxAmount = newItem.stacks;
                    int sum = newItem.amount + item.amount;
                    // they fit in one slot
                    if (sum <= maxAmount)
                    {
                        item.amount = sum;
                        TriggerItemChangeUpdate();

                        RemoveItem(newItem);
                    }
                    // one has max -> swap
                    else if (newItem.amount == maxAmount || item.amount == maxAmount)
                    {
                        Swap(itemIndex, newLocationIndex);
                        TriggerItemChangeUpdate();
                    }
                    // they don't fit
                    else
                    {
                        item.amount = maxAmount;
                        newItem.amount = sum - maxAmount;
                        TriggerItemChangeUpdate();
                    }
                }
                // if they are different type, swap them
                else
                {
                    Swap(itemIndex, newLocationIndex);
                    TriggerItemChangeUpdate();
                }
            }
        }

        private void Swap(int indexA, int indexB)
        {
            Item tmp = items[indexA];
            items[indexA] = items[indexB];
            items[indexB] = tmp;
        }

        // use item is currently only working for Ammo type item
        // if other items become usable, this method should be refactored
        public void UseItem(Item item)
        {
            if (item.name.Equals("Ammo"))
            {
                FireBullets fireBullets = player.GetComponent<FireBullets>();
                int numberOfBullets = fireBullets.GetBulletsNumber();
                int maxNumberOfBullets = fireBullets.maxAmmo;
                int diff = maxNumberOfBullets - numberOfBullets;
                // if there is space in current amunition stack
                if (diff > 0)
                {
                    int newBulletsAvailable = item.amount;
                    if (diff >= newBulletsAvailable) // if it fits everything selected from inventory
                    {
                        fireBullets.AddBulletsToStack(newBulletsAvailable);
                        RemoveItem(item);
                    }
                    else
                    {
                        fireBullets.AddBulletsToStack(diff);
                        item.amount -= diff;
                        TriggerItemChangeUpdate();
                    }
                }
                else
                {
                    Debug.Log("No more space in current ammo stack!");
                }
            }
        }

        public void SplitItem(Item item)
        {
            // can't split if it's not more than 1
            if (item.amount > 1)
            {
                int firstAmount = item.amount / 2;
                int secondAmount = item.amount - firstAmount;
                item.amount = firstAmount;
                TriggerItemChangeUpdate();

                Item newItem = ScriptableObject.CreateInstance<Item>();
                item.CopyItemAttributes(newItem);
                newItem.amount = secondAmount;
                items.Add(newItem);
                TriggerItemChangeUpdate();
            }
        }
    }
}
