using UnityEngine;
using System.Collections;

public class VirtualJoystick : MonoBehaviour {

	public float anchorDrift = 0.75f;
	private Vector2 anchorPoint;
	private bool active;

	public VirtualJoystick(float drift = 0.75f)
	{
		anchorDrift = drift;
	}

	public void Begin(Vector2 point)
	{
		anchorPoint = point;
	}

	public Vector2 UpdateJoystick(Vector2 point)
	{
		Vector2 diff = (point - anchorPoint).normalized;
		anchorPoint += (diff * anchorDrift);
		return diff;
	}


}
