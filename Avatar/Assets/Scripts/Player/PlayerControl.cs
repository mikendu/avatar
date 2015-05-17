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
	private VirtualJoystick rightJoystick;

	private void Awake()
	{
		// Set up the reference.
		player = GetComponent<Player>();
		arrow = GetComponent<ArrowControl>();
		moveDirection = new Vector2();
		arrow.Hide();

		// Virtual input
		leftJoystick = new VirtualJoystick();
		rightJoystick = new VirtualJoystick();

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
		switch (side) {
			
			case ScreenSide.Left:
				leftJoystick.Begin(point);	
				break;
				
			case ScreenSide.Right:
				rightJoystick.Begin(point);
				arrow.Show();
				break;

			case ScreenSide.None:
			default:
				break;
		}
	}
	
	public void OnInputUp(Vector2 point)
	{
		ScreenSide side = ScreenUtils.GetSide (point);
		switch (side) {
			
			case ScreenSide.Left:
				moveDirection.Set (0, 0);	
				break;
				
			case ScreenSide.Right:
				arrow.Hide();
				Vector2 direction = rightJoystick.UpdateJoystick(point);
				player.Dash(direction);
				break;
				
			case ScreenSide.None:
			default:
				break;
		}
	}
	
	public void OnInputDrag(Vector2 point, Vector2 delta)
	{
		ScreenSide side = ScreenUtils.GetSide(point);
		switch (side) {

			case ScreenSide.Left:
				// Get joystick drag direction
				Vector2 directionLeft = leftJoystick.UpdateJoystick(point);		
				moveDirection = directionLeft;
				break;

			case ScreenSide.Right:
				Vector2 directionRight = rightJoystick.UpdateJoystick(point);	
				arrow.SetDirection(directionRight);	
				break;

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

