using UnityEngine;
using System.Collections;

public class VirtualJoystick {

	public float anchorDrift = 0.75f;
	private Vector2 lastPoint;
	private Vector2 anchorPoint;
	private Vector2 currentDirection;
    private float currentMagnitude;
	private int inputIndex;

	public VirtualJoystick(float drift = 0.75f)
	{
		anchorDrift = drift;
		ForceReset();
	}

	public void Snap()
	{
		anchorPoint = lastPoint;
		currentDirection = Vector2.zero;
        currentMagnitude = 0;
	}

	public void Begin(Vector2 point, int index)
	{
		anchorPoint = point;
		inputIndex = index;
		currentDirection = Vector2.zero;
        currentMagnitude = 0;
    }

	public bool Tracking(int index)
	{
		return (inputIndex == index);
	}

	public bool Reset(int index)
	{
		bool tracking = Tracking (index);
		if(tracking)
			ForceReset();

		return tracking;
	}

	public void ForceReset()
	{
		inputIndex = -1;
		currentDirection = Vector2.zero;
        currentMagnitude = 0;
    }

	public bool ProcessInput(Vector2 point, int index)
	{
		bool tracking = Tracking (index);
		if(tracking)
		{
			lastPoint = point;
            currentDirection = (point - anchorPoint);
            currentMagnitude = currentDirection.magnitude;
            currentDirection = currentDirection.normalized;
			anchorPoint += (currentDirection * anchorDrift);
		}

		return tracking;
	}

    public float GetMagnitude()
    {
        return currentMagnitude;
    }

	public Vector2 GetDirection()
	{
		return currentDirection;
	}

}
