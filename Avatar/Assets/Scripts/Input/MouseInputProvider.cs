using UnityEngine;
using System.Collections;

public class MouseInputProvider : InputProvider {

	private Vector2 lastMousePosition;
	private bool mouseDown;

	private void Awake()
	{
#if ((UNITY_IPHONE  || UNITY_ANDROID) && !UNITY_EDITOR)
		this.enabled = false;
#endif
	}

	// Update is called once per frame
	void Update () 
	{
		bool down = Input.GetMouseButton(0);
		Vector2 mousePos = Input.mousePosition;

		// Went from mouse up to mouse down -> "Down" Event
		if(down)
		{
			// Check if mouse is in screen area
			if(ScreenUtils.InScreen(mousePos))
			{
				// Mouse was alread down -> "Drag" Event
				if(mouseDown)
					RaiseInputEvent(InputEvent.Drag, mousePos, 
					                mousePos - lastMousePosition);

				// Mouse was previously up -> "Down" Event
				else
					RaiseInputEvent(InputEvent.Down, mousePos, Vector2.zero);
			}

			// Mouse was dragged off the screen -> "Exit" event
			else if(mouseDown)
			{
				mouseDown = false;
				RaiseInputEvent(InputEvent.Exit, mousePos, Vector2.zero);
				return;
			}
		}
		else
		{
			// Went from mouse down to mouse up -> "Up" event
			if(mouseDown)
				RaiseInputEvent(InputEvent.Up, mousePos, Vector2.zero);
		}

		// Update tracking vars
		lastMousePosition = mousePos;
		mouseDown = down;
	}


}
