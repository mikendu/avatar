using UnityEngine;
using System.Collections;


public enum ScreenSide { Left, Right, None };

public class ScreenUtils {


	public static bool InScreen(Vector2 point)
	{
		bool validX = (point.x >= 0) && (point.x < Screen.width);
		bool validY = (point.y >= 0) && (point.y < Screen.height);
		
		return (validX && validY);
	}

	public static ScreenSide GetSide(Vector2 point)
	{
		float halfWidth = Screen.width / 2.0f;
		bool left = (point.x >= 0 && point.x < halfWidth);
		bool right = (point.x >= halfWidth && point.x < Screen.width);
		bool validY = (point.y >= 0 && point.y < Screen.height);

		// If it's outisde the scren, it's on neither side
		if (!validY || (!left && !right))
			return ScreenSide.None;
		else 
			return (left) ? ScreenSide.Left : ScreenSide.Right;
	}
}
