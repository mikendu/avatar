using UnityEngine;
using System.Collections;

public class TouchInputProvider : InputProvider {

	private void RaiseTouchEvent(Touch touch)
	{
		switch(touch.phase)
		{
			case TouchPhase.Began:
				RaiseInputEvent(InputEvent.Down, touch.position, Vector2.zero);
				break;

			case TouchPhase.Ended:
				RaiseInputEvent(InputEvent.Up, touch.position, Vector2.zero);
				break;

			case TouchPhase.Moved:
				RaiseInputEvent(InputEvent.Drag, touch.position, 
			                	touch.deltaPosition);
				break;

			case TouchPhase.Canceled:
			RaiseInputEvent(InputEvent.Exit, touch.position, Vector2.zero);
				break;

			default:
				break;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach (Touch touch in Input.touches) {
			RaiseTouchEvent(touch);
		}
	}
}
