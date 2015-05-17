using UnityEngine;
using System.Collections;

public interface IInputEventHandler {

	void OnInputDown(Vector2 point, int inputIndex);
	void OnInputUp(Vector2 point, int inputIndex);
	void OnInputDrag(Vector2 point, Vector2 delta, int inputIndex);
	void OnInputExit(int inputIndex);


}
