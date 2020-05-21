using UnityEngine;
using CG.UI;
using CG.Core;

namespace CG.Control
{
    public class CameraMovement : MonoBehaviour
    {
        public float cameraSensitivity = 100f;
        public float cameraMaxDistanceView = 5f;

        public Transform player;

        Inventory inventory;
        float xRotation = 0f;
        bool hitFlag = false;
        PickupItem lastPickup = null;

        int numberOfPickups;
        public delegate void OnItemPickedUp();
        public OnItemPickedUp onItemPickedUpCallback;

        private void Start()
        {
            numberOfPickups = 0;
            Cursor.visible = false;
            inventory = Inventory.instance;
        }

        private void Update()
        {
            if (inventory.isOpened)
                return;
            RotationUpdate();
            PickupsUpdate();
        }

        private void TriggerItemPickedUpUpdate()
        {
            if (onItemPickedUpCallback != null)
                onItemPickedUpCallback.Invoke();
        }

        private void PickupsUpdate()
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, cameraMaxDistanceView))
            {
                if (hitInfo.transform.CompareTag("Pickup"))
                {
                    PickupItem pickupItem = hitInfo.transform.GetComponent<PickupItem>();
                    pickupItem.ShowInfo();
                    hitFlag = true;
                    lastPickup = pickupItem;
                    if (Input.GetButtonDown("Pickup"))
                    {
                        pickupItem.PickUp();
                        numberOfPickups++;
                        TriggerItemPickedUpUpdate();
                        // if picked up, should restart flags
                        hitFlag = false;
                        lastPickup = null;
                    }
                }
                else if (hitInfo.transform.CompareTag("Chest"))
                {
                    if (Input.GetButtonDown("Pickup"))
                    {
                        Chest chest = hitInfo.transform.GetComponent<Chest>();
                        if (chest.IsPickable())
                        {
                            chest.PickUp();
                        }
                    }
                }
                else
                {
                    // Hitting something else
                    hitFlag = false;
                    if (lastPickup != null)
                    {
                        lastPickup.HideInfo();
                        lastPickup = null;
                    }
                }
            }
            else if (hitFlag)
            {
                // No hits
                hitFlag = false;
                if (lastPickup != null)
                {
                    lastPickup.HideInfo();
                    lastPickup = null;
                }
            }
        }

        private void RotationUpdate()
        {
            float cameraX = Input.GetAxis("Mouse X") * Time.deltaTime * cameraSensitivity;
            float cameraY = Input.GetAxis("Mouse Y") * Time.deltaTime * cameraSensitivity;

            xRotation -= cameraY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            player.Rotate(Vector3.up * cameraX);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}