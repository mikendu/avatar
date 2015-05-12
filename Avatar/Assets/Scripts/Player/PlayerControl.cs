using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour, IInputEventHandler
{
	public List<InputProvider> inputProviders = new List<InputProvider>(); // Scripts that generates input events

	private Vector2 moveDirection; // Vector tracking current move direction
	private Player player; // Reference to the player actual character script.
	private VectorBuffer inputBuffer;
	

	private void Awake()
	{
		// Set up the reference.
		player = GetComponent<Player>();
		moveDirection = new Vector2();
		inputBuffer = new VectorBuffer(5);

		// Regist this as an event handler
		foreach(InputProvider provider in inputProviders)
			provider.RegisterHandler(this);
	}

	private void Update()
	{
		player.Move(moveDirection); 
	}


	// -- Event Handling Functions -- //

	public void OnInputDown(Vector2 point)
	{
	}
	
	public void OnInputUp(Vector2 point)
	{
		moveDirection.Set (0, 0);
	}
	
	public void OnInputDrag(Vector2 point, Vector2 delta)
	{
		ScreenSide side = ScreenUtils.GetSide(point);
		switch (side) {

			case ScreenSide.Left:
				inputBuffer.PushVector(delta);
				Vector2 avg = inputBuffer.GetAverage();
				
				// Calculate angle from drag direction
				float angle = Mathf.Atan2(avg.y, avg.x);

				// Wrap angle to [0, 360) and round to nearest 45 degrees
				angle = MathUtils.WrapAngle(angle, 0.0f,  2.0f * Mathf.PI);
				angle = MathUtils.RoundAngle(angle, Mathf.PI / 4.0f);					
				
				// Get the vector corresponding to the rounded angle
				float x = Mathf.Cos(angle);
				float y = Mathf.Sin(angle);
				Vector2 direction = new Vector2(x, y);
				
				// Set this as the move direction
				moveDirection = direction;
				break;

			case ScreenSide.Right:
			case ScreenSide.None:
			default:
				break;
		}
	}
	
	public void OnInputExit()
	{
		moveDirection.Set (0, 0);
	}


}

