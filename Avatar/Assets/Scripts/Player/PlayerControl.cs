using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour, IInputEventHandler
{
	public List<InputProvider> inputProviders = new List<InputProvider>(); // Scripts that generates input events

	private Vector2 moveDirection; // Vector tracking current move direction
	private Player player; // Reference to the player actual character script.
	private ArrowControl arrow; 
	private VirtualJoystick leftJoystick;
	

	private void Awake()
	{
		// Set up the reference.
		player = GetComponent<Player>();
		arrow = GetComponent<ArrowControl>();
		moveDirection = new Vector2();

		// Virtual input
		leftJoystick = new VirtualJoystick();

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
		ScreenSide side = ScreenUtils.GetSide (point);

		if (side == ScreenSide.Left)
			leftJoystick.Begin(point);
	}
	
	public void OnInputUp(Vector2 point)
	{
		ScreenSide side = ScreenUtils.GetSide (point);

		if(side == ScreenSide.Left)
			moveDirection.Set (0, 0);
	}
	
	public void OnInputDrag(Vector2 point, Vector2 delta)
	{
		ScreenSide side = ScreenUtils.GetSide(point);
		switch (side) {

			case ScreenSide.Left:
				// Get joystick drag direction
				Vector2 direction = leftJoystick.Update(point);				
				arrow.SetDirection(direction);	
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

