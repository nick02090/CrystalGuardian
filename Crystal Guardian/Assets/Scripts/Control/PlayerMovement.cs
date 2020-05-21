using UnityEngine;
using CG.Core;

namespace CG.Control
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 10f;
        public float gravity = -9.81f;
        public float jumpHeight = 5f;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        public Transform groundCheck;
        public CharacterController controller;

        public Vector3 velocity;
        public bool isGrounded;

        Inventory inventory;

        private void Start()
        {
            inventory = Inventory.instance;
        }

        private void Update()
        {
            if (inventory.isOpened)
                return;

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            controller.Move(move * Time.deltaTime * speed);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight / 10000 * -2f * gravity);
            }

            velocity.y += gravity * Mathf.Pow(Time.deltaTime, 2);
            controller.Move(velocity);
        }
    }
}
