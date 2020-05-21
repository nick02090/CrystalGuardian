using UnityEngine;
using CG.Core;

namespace CG.UI
{
    public class InventoryUI : MonoBehaviour
    {
        public Transform itemsParent;
        public GameObject inventoryUI;
        public GameObject inventoryGuideUI;

        Inventory inventory;

        InventorySlot[] slots;

        private void Start()
        {
            inventory = Inventory.instance;
            inventory.onItemChangedCallback += UpdateUI;

            slots = itemsParent.GetComponentsInChildren<InventorySlot>();
            inventoryUI.SetActive(false);
            inventoryGuideUI.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Inventory"))
            {
                bool activeState = !inventoryUI.activeSelf;
                inventoryUI.SetActive(activeState);
                inventoryGuideUI.SetActive(activeState);
                Cursor.visible = activeState;
                inventory.isOpened = activeState;
            }
        }

        private void UpdateUI()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < inventory.items.Count)
                {
                    slots[i].AddItem(inventory.items[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }
    }
}
