using UnityEngine;
using UnityEngine.EventSystems;

namespace CG.UI
{
    public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        // Called by UI trigger
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        // Called by UI trigger
        public void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = Vector3.zero;
        }
    }
}
