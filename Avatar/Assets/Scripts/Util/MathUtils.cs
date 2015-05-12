using UnityEngine;
using System.Collections;

public class MathUtils  {

	// Wraps the given angle to be within the give bounds
	public static float WrapAngle(float angle, float min, float max)
	{
		float diff = (max - min);
		angle = (angle < min) ? (angle + diff) : angle;
		angle = (angle > max) ? (angle - diff) : angle;

		return angle;
	}

	// Rounds the given angle to closest given increment
	public static float RoundAngle(float angle, float increment)
	{
		angle += (increment / 2.0f);
		angle -= (angle % increment);

		return angle;
	}
}
