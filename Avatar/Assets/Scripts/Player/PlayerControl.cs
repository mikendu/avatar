using UnityEngine;
using Assets.Scripts.Actor;

public class PlayerControl : MonoBehaviour, IInputEventHandler, IPlayerEventListener
{

	
	private VirtualJoystick leftJoystick;
	private VirtualJoystick rightJoystick;

    private IWeapon weapon = null;
    private IMovement movement = null;

    private VectorBuffer movementInputBuffer = new VectorBuffer(3);
    private Vector2 movementDirection;
    private float movementSpeed;
    

    private void Awake()
	{
        // Find the movement and weapon componenets (if any)
        weapon = this.gameObject.GetComponent<IWeapon>();
        movement = this.gameObject.GetComponent<IMovement>();

		// Virtual input
		leftJoystick = new VirtualJoystick(0.75f);
		rightJoystick = new VirtualJoystick(0.4f);

		// Regist this as an event handler for input
		foreach(InputProvider provider in GameManager.GetInputProviders())
			provider.RegisterHandler(this);
	}

	private void Start()
	{	
	}

	private void Update()
	{
        movement.Move(movementDirection, movementSpeed);
    }


	// Event handler for when the player object
	// completes a "dash" action
	public void OnDashComplete()
	{
		leftJoystick.Snap();
	}


	// -- Event Handling Functions -- //
	public void OnInputDown(Vector2 point, int inputIndex)
	{
		ScreenSide side = ScreenUtils.GetSide (point);
		switch (side) {
			
			case ScreenSide.Left:
				leftJoystick.Begin(point, inputIndex);
                (movement as MonoBehaviour).enabled = true;
                resetMovement();
				break;
				
			case ScreenSide.Right:
                rightJoystick.Begin(point, inputIndex);
                if (weapon != null)
                    weapon.Begin(point);
				break;

			case ScreenSide.None:
			default:
				break;
		}
	}
	
	public void OnInputUp(Vector2 point, int inputIndex)
	{
        if (leftJoystick.Reset(inputIndex))
        {
            (movement as MonoBehaviour).enabled = false;
            resetMovement();
        }

        if (rightJoystick.Tracking(inputIndex))
		{
            Vector2 direction = rightJoystick.GetDirection();
			rightJoystick.Reset(inputIndex);
            if (weapon != null)
                weapon.Execute(direction);
        }
	}
	
	public void OnInputDrag(Vector2 point, Vector2 delta, int inputIndex)
	{
		if(leftJoystick.ProcessInput(point, inputIndex))
		{
            float magnitude = delta.magnitude;
            if (magnitude > 0.5f)
            {
                movementInputBuffer.PushVector(delta);
            }

            this.movementDirection = movementInputBuffer.GetAverage();
            this.movementSpeed = magnitude / 3.0f;
		}

		if(rightJoystick.ProcessInput(point, inputIndex))
		{
			Vector2 direction = rightJoystick.GetDirection();
            if (weapon != null)
                weapon.Target(direction);
        }
	}
	
	public void OnInputExit(int inputIndex)
	{
        if (leftJoystick.Reset(inputIndex))
        {
            resetMovement();
        }
	
		if(rightJoystick.Reset(inputIndex))
		{
            if (weapon != null)
                weapon.Reset();
        }
	}

    private void resetMovement()
    {
        movementInputBuffer.Reset();
        movementDirection = Vector2.zero;
        movementSpeed = 0.0f;
    }


}

