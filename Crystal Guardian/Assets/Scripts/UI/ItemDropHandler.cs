using UnityEngine;
using UnityEngine.EventSystems;
using CG.Core;

namespace CG.UI
{
    public class ItemDropHandler : MonoBehaviour, IDropHandler
    {
        // Called by UI trigger
        public void OnDrop(PointerEventData eventData)
        {
            RectTransform inventoryPanel = transform as RectTransform;

            Transform icon = eventData.pointerDrag.transform;
            Transform inventorySlot = icon.parent.transform;
            Transform itemsParent = inventorySlot.parent.transform;
            int? index = GetInventoryIndex(itemsParent, inventorySlot);
            if (!index.HasValue)
            {
                Debug.LogError("Error while finding index!");
                return;
            }

            // if it drops outside of inventory
            if (!RectTransformUtility.RectangleContainsScreenPoint(inventoryPanel, Input.mousePosition))
            {
                Item item = Inventory.instance.items[index.Value];
                Inventory.instance.RemoveItem(item);
            }
            else // if it drops inside of inventory
            {
                for (int i = 0; i < itemsParent.childCount; i++)
                {
                    RectTransform inventorySlotPanel = itemsParent.GetChild(i) as RectTransform;
                    if (RectTransformUtility.RectangleContainsScreenPoint(inventorySlotPanel, Input.mousePosition))
                    {
                        Inventory.instance.PutItemOn(index.Value, i);
                        return;
                    }
                }
            }
        }

        private int? GetInventoryIndex(Transform itemsParent, Transform inventorySlot)
        {
            for (int i = 0; i < itemsParent.childCount; i++)
            {
                Transform currentInventorySlot = itemsParent.GetChild(i);
                if (currentInventorySlot.GetInstanceID() == inventorySlot.GetInstanceID())
                {
                    return i;
                }
            }
            return null;
        }
    }
}
