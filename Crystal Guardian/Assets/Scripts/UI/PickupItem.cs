using UnityEngine;
using CG.Core;

namespace CG.UI
{
    public class PickupItem : MonoBehaviour
    {
        public Item item;
        public DisplayUI displayUI;

        public void PickUp()
        {
            Item newItem = ScriptableObject.CreateInstance<Item>();
            item.CopyItemAttributes(newItem);
            bool isPickedUp = Inventory.instance.AddItem(newItem);
            if (isPickedUp)
            {
                // Destroying parent because it holds pickup item object and display UI
                Destroy(gameObject.transform.parent.gameObject);
            }
        }

        public void ShowInfo()
        {
            displayUI.SetTextState(true);
        }

        public void HideInfo()
        {
            displayUI.SetTextState(false);
        }
    }
}
