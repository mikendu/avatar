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

            float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

			/*
			int h = 0;
			h -= Input.GetAxisRaw("left") ? 1 : 0;
			h += Input.GetKey("right") ? 1 : 0;

			int v = 0;
			v += Input.GetKey("up") ? 1 : 0;
			v -= Input.GetKey("down") ? 1 : 0;*/

            // calculate move direction
            moveDirection = ((v * Vector2.up) + (h * Vector2.right)).normalized;      
			player.Move(moveDirection);
        }


        private void FixedUpdate()
        {
            // Call the Move function of the player controller
            
        }
    }
}
