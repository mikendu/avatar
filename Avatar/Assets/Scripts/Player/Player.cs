using System;
using UnityEngine;


public class Player : MonoBehaviour
{
	private static float DURATION = 0.5f;
	private static float DASH_MAX_DURATION = 0.5f;

    public float moveSpeed = 5; // The speed that the player moves at
	public float dashDistance = 700;
	public float dashSpeed = 50;

    private Rigidbody2D physicsbody;
	private bool dashing; 
	private float dashed;
	private float dashTime;
	private Vector2 lastPosition;

	private float slowTimer = 0.0f;
	private Vector2 startVelocity;

    private void Start()
    {
		physicsbody = GetComponent<Rigidbody2D>();
		dashing = false;
		dashed = 0.0f;
		dashTime = 0.0f;
    }

	void Update()
	{
		if(dashing)
		{
			Vector2 position = physicsbody.position;
			float diff = (position - lastPosition).magnitude;

			dashed += diff;
			dashTime += Time.deltaTime;

			lastPosition = position;

			if(dashed > dashDistance || dashTime > DASH_MAX_DURATION)
			{
				dashed = 0.0f;
				dashTime = 0.0f;
				dashing = false;
			}
		}
	}

	void FixedUpdate()
	{

	}

	private void SetVelocity(Vector2 velocity)
	{
		float speed = velocity.magnitude;
		if(speed == 0.0f && slowTimer < DURATION)
		{
			physicsbody.velocity = Vector2.ClampMagnitude(physicsbody.velocity, moveSpeed);
			startVelocity = (slowTimer == 0.0f) ? physicsbody.velocity : startVelocity;
			physicsbody.velocity = Vector2.Lerp(startVelocity, Vector2.zero, (slowTimer / DURATION));
			slowTimer += Time.deltaTime;
		}
		else
		{
			slowTimer = 0.0f;
			physicsbody.velocity = velocity;
		} 
	}


    public void Move(Vector2 moveDirection)
    {
		if(!dashing) 
			SetVelocity(moveDirection * moveSpeed);    	

    }

	public void Dash(Vector2 dashDirection)
	{
		dashing = true;
		dashed = 0.0f;
		dashTime = 0.0f;
		lastPosition = physicsbody.position;
		SetVelocity(dashDirection * dashSpeed);
	}

}
