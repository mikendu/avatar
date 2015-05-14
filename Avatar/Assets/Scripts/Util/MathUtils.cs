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

	// Rounds the given angle to closest given interval
	public static float RoundAngle(float angle, float interval)
	{
		angle += (interval / 2.0f);
		angle -= (angle % interval);

		return angle;
	}

	/* Rounds the given angle to the closest given interval, 
	 * but with a bias either towards or away from (interval * 2). 
	 * Biasing makes the round at closer/farther values from the interval. 
	 * For example, normally, an angle that less than halfway between 
	 * interval and (interval * 2) would round to the value interval. However, with
	 * biasing, this can change (i.e. the rounding gets biased towards interval * 2).
	 * 
	 * Positive bias will make values bias towards interval * 2.
	 * For example, if the interval is PI/4, and a bias > 0 is provided
	 * the rounding will be biased towards the PI/2 values. If a negative bias
	 * is provided, the rounding will be biased towards the PI/4 values (and away from PI/2).
	 * 
	 * Note that when bias of 0 is provided, this method is equivalent to the normal
	 * RoundAngle function above.
	 * 
	 * @param angle The angle to round
	 * @param interval The interval to round the angle to. This should be some rational
	 * 					division of PI (ex PI/3, PI/4, etc), however this will 
	 * 					not be enforced. 
	 * @param bias A float indicating the direction and amount of the bias.
	 * 				This value should be between -1.0 and 1.0, and will be 
	 * 				clamped to that interval if it is not.
	 * 
	 * @return The rounded angle.
	 */
	public static float RoundAngleBias(float angle, float interval, float bias)
	{
		bias = Mathf.Clamp(bias, -1.0f, 1.0f);
		float remainder = (angle % (interval * 2));
		float halfInterval = (interval / 2);
		float addition = 0.0f;

		// Case 1, angle is between an (interval * 2) value
		// and an interval value
		if(remainder < interval)
			addition = (halfInterval * (1.0f - bias));
				
		// Case 2, angle is between interval value
		// and an (interval * 2) value
		else if(remainder > interval)
			addition = (halfInterval * (1.0f + bias));

		// Case 3, angle is exactly on an interval value - 
		// no work required, addition should be zero.

		// Round the angle with the bias
		angle += addition;
		angle -= (angle % interval);

		return angle;


	}
}
