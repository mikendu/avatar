using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class InputProvider : MonoBehaviour {

	protected enum InputEvent { Down, Up, Hold, Drag, Exit };

	public float DragThreshold = 2.0f;
	public List<IInputEventHandler> InputHandlers = new List<IInputEventHandler>();


	public void RegisterHandler (IInputEventHandler handler)
	{
		InputHandlers.Add(handler);
	}

	protected void RaiseInputEvent(InputEvent eventType, Vector2 point,
	                        		Vector2 delta)
	{
		switch(eventType)
		{
			// Input down
			case InputEvent.Down:
			
				foreach(IInputEventHandler handler in InputHandlers)
					handler.OnInputDown(point);
				break;

			// Input up
			case InputEvent.Up:
			
				foreach(IInputEventHandler handler in InputHandlers)
					handler.OnInputUp(point);
				break;

			// Input dragged
			case InputEvent.Drag:

				// Threshold on "drag" events
				if(delta.magnitude > DragThreshold)
				{
					foreach(IInputEventHandler handler in InputHandlers)
						handler.OnInputDrag(point, delta);
				}
				break;

			// Input removed (ex off screen)
			case InputEvent.Exit:
				
				foreach(IInputEventHandler handler in InputHandlers)
					handler.OnInputExit();
				break;

			default:
				break;
		}
	}
}
