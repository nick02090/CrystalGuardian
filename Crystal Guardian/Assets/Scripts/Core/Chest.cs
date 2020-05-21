using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CG.Core
{
    public class Chest : MonoBehaviour
    {
        public GameObject bullets;
        public Text text;
        public Item item;

        int seconds;
        bool isPickable;
        Inventory inventory;

        private void Start()
        {
            isPickable = true;
            bullets.SetActive(isPickable);
            text.text = "Grab ammo now [E]";
            inventory = Inventory.instance;
        }

        public void PickUp()
        {
            isPickable = false;
            bullets.SetActive(isPickable);
            seconds = 11;
            Item newItem = ScriptableObject.CreateInstance<Item>();
            item.CopyItemAttributes(newItem);
            newItem.name = "Ammo";
            newItem.stacks = 100;
            inventory.AddItem(newItem);
            StartCoroutine(FillChest());
        }

        private IEnumerator FillChest()
        {
            while (seconds > 0)
            {
                seconds -= 1;
                text.text = $"Come back in {seconds}s";
                yield return new WaitForSeconds(1f);
            }
            isPickable = true;
            bullets.SetActive(isPickable);
            text.text = "Grab ammo now [E]";
        }

        public bool IsPickable()
        {
            return isPickable;
        }
    }
}