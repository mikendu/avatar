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

		// Virtual input
		leftJoystick = new VirtualJoystick();
		rightJoystick = new VirtualJoystick();

		// Regist this as an event handler
		foreach(InputProvider provider in inputProviders)
			provider.RegisterHandler(this);
	}

	private void Start()
	{	
		arrow.Hide();
	}

	private void Update()
	{
		player.Move(moveDirection); 
	}


	// -- Event Handling Functions -- //

	public void OnInputDown(Vector2 point, int inputIndex)
	{
		ScreenSide side = ScreenUtils.GetSide (point);
		switch (side) {
			
			case ScreenSide.Left:
				leftJoystick.Begin(point, inputIndex);	
				break;
				
			case ScreenSide.Right:
				rightJoystick.Begin(point, inputIndex);
				arrow.Show();
				break;

			case ScreenSide.None:
			default:
				break;
		}
	}
	
	public void OnInputUp(Vector2 point, int inputIndex)
	{
		if(leftJoystick.Reset(inputIndex))
			moveDirection.Set(0,0);

		if(rightJoystick.Tracking(inputIndex))
		{
			arrow.Hide();
			Vector2 direction = rightJoystick.GetDirection();
			player.Dash(direction);
			rightJoystick.Reset(inputIndex);
		}
	}
	
	public void OnInputDrag(Vector2 point, Vector2 delta, int inputIndex)
	{
		if(leftJoystick.ProcessInput(point, inputIndex))
		{
			Vector2 direction = leftJoystick.GetDirection();
			moveDirection = direction;
		}
		
		if(rightJoystick.ProcessInput(point, inputIndex))
		{
			Vector2 direction = rightJoystick.GetDirection();
			arrow.SetDirection(direction);	
		}
	}
	
	public void OnInputExit(int inputIndex)
	{
		if(leftJoystick.Reset(inputIndex))
			moveDirection.Set(0,0);
		
		if(rightJoystick.Reset(inputIndex))
			arrow.Hide();
	}


}

