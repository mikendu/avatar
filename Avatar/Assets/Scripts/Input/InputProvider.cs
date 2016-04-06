using UnityEngine;
using System.Collections.Generic;

/**
    This class defines an interface and common methods
    for methods of retrieving user input. User input 
    can be retrieved from the touch screen, mouse,
    or keyboard, depending on the platform and system.

    */
public abstract class InputProvider : MonoBehaviour {

	protected enum InputEvent { Down, Up, Hold, Drag, Exit };

	public float DragThreshold = 2.0f;
	public List<IInputEventHandler> InputHandlers = new List<IInputEventHandler>();


	public void RegisterHandler (IInputEventHandler handler)
	{
		InputHandlers.Add(handler);
	}

	protected void RaiseInputEvent(InputEvent eventType, Vector2 point,
	                        		Vector2 delta, int inputIndex = 0)
	{
		switch(eventType)
		{
			// Input down
			case InputEvent.Down:
			
				foreach(IInputEventHandler handler in InputHandlers)
					handler.OnInputDown(point, inputIndex);
				break;

			// Input up
			case InputEvent.Up:
			
				foreach(IInputEventHandler handler in InputHandlers)
					handler.OnInputUp(point, inputIndex);
				break;

			// Input dragged
			case InputEvent.Drag:

				// Threshold on "drag" events
				if(delta.magnitude > DragThreshold)
				{
					foreach(IInputEventHandler handler in InputHandlers)
						handler.OnInputDrag(point, delta, inputIndex);
				}
				break;

			// Input removed (ex off screen)
			case InputEvent.Exit:
				
				foreach(IInputEventHandler handler in InputHandlers)
					handler.OnInputExit(inputIndex);
				break;

			default:
				break;
		}
	}
}
