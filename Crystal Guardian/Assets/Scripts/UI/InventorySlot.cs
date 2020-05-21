using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using CG.Core;

namespace CG.UI
{
    public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Image icon;
        public Text text;
        public Text tooltip;

        Item item;
        bool isMouseOver = false;
        bool isHoldingShift = false;

        private void Update()
        {
            if (isMouseOver)
            {
                if (Input.GetButtonDown("Pickup"))
                {
                    UseItem();
                }
            }
            if (Input.GetButton("SplitItem"))
            {
                isHoldingShift = true;
            }
            else
            {
                isHoldingShift = false;
            }
        }

        public void AddItem(Item newItem)
        {
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;
            text.text = $"{item.amount}x";
            text.enabled = true;
            isMouseOver = false;
            tooltip.enabled = false;
        }

        public void ClearSlot()
        {
            item = null;

            icon.sprite = null;
            icon.enabled = false;
            text.text = "";
            text.enabled = false;
            isMouseOver = false;
            tooltip.enabled = false;
        }

        public void ShowData()
        {
            if (item != null)
            {
                if (item.name.Equals("Ammo"))
                {
                    tooltip.enabled = true;
                    isMouseOver = true;
                }
            }
        }

        public void HideData()
        {
            if (item != null)
            {
                if (item.name.Equals("Ammo"))
                {
                    tooltip.enabled = false;
                    isMouseOver = false;
                }
            }
        }

        public void UseItem()
        {
            if (item != null)
            {
                item.Use();
            }
        }

        // Called by UI trigger
        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowData();
        }

        // Called by UI trigger
        public void OnPointerExit(PointerEventData eventData)
        {
            HideData();
        }

        // Called by UI trigger
        public void OnPointerClick(PointerEventData eventData)
        {
            if (item != null)
            {
                if (eventData.button == PointerEventData.InputButton.Right && isHoldingShift)
                {
                    item.Split();
                }
            }
        }
    }
}
