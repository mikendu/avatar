using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float mMovePower = 5; // The force added to the player to move it.
        private Rigidbody2D mRigidbody;


        private void Start()
        {
            mRigidbody = GetComponent<Rigidbody2D>();
			mRigidbody.drag = 0.9f;
        }


        public void Move(Vector2 moveDirection)
        {
        	mRigidbody.AddForce(moveDirection * mMovePower);
        }

		private void FixedUpdate()
		{

		}
    }
}
