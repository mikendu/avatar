using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class PlayerControl : MonoBehaviour
    {
        private Player player; // Reference to the player controller.
		private Vector2 moveDirection; // Vector tracking current move direction


        private void Awake()
        {
            // Set up the reference.
            player = GetComponent<Player>();
        }


        private void Update()
        {
            // Get the axis and jump input.

            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

            // calculate move direction
            moveDirection = ((v * Vector2.up) + (h * Vector2.right)).normalized;        
        }


        private void FixedUpdate()
        {
            // Call the Move function of the player controller
            player.Move(moveDirection);
        }
    }
}
