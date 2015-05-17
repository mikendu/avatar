using UnityEngine;
using System.Collections;

public class ArrowControl : MonoBehaviour {

	public Transform arrowObject;
	public float arrowDistance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void SetRotation(float angle)
	{
		float angleDegree = angle * Mathf.Rad2Deg;
		arrowObject.localRotation = Quaternion.AngleAxis(angleDegree, Vector3.forward);
	}

	private void SetPosition(Vector2 position)
	{
		arrowObject.transform.localPosition = new Vector3(position.x, position.y);
	}

	public void SetDirection(float angle)
	{
		SetRotation (angle);

		float x = arrowDistance * Mathf.Cos (angle);
		float y = arrowDistance * Mathf.Sin (angle);
		SetPosition (new Vector2 (x, y));

	}

	public void SetDirection(Vector2 direction)
	{
		// Calculate angle from joystick drag direction
		float angle = Mathf.Atan2 (direction.y, direction.x);
		angle = MathUtils.WrapAngle (angle, 0.0f, 2.0f * Mathf.PI);	
		SetRotation (angle);

		SetPosition (arrowDistance * direction.normalized);
	}
}
