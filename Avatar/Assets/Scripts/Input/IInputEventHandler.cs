using UnityEngine;
using System.Collections;

public interface IInputEventHandler {

	void OnInputDown(Vector2 point);
	void OnInputUp(Vector2 point);
	void OnInputDrag(Vector2 point, Vector2 delta);
	void OnInputExit();


}
