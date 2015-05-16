using System;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private float mMoveSpeed = 5; // The speed that the player moves at
    private Rigidbody2D mRigidbody;

    private void Start()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
    }


    public void Move(Vector2 moveDirection)
    {
		mRigidbody.velocity = (moveDirection * mMoveSpeed);    	
    }

}
