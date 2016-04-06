using UnityEngine;
using System.Collections;

public class KeyboardInputProvider : MonoBehaviour {

	private void Awake()
	{
#if ((UNITY_IPHONE  || UNITY_ANDROID) && !UNITY_EDITOR)
		this.enabled = false;
#endif
	}

	// Update is called once per frame
	void Update () 
	{
		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");

        //if (x != 0 || y != 0)
            //player.Move(new Vector2(x, y).normalized, 1.0f);
	}


}
