using System;
using UnityEngine;



public class CameraControl : MonoBehaviour
{
    public Transform target;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;
	
	public float shakeFactor = 1.0f;
	public float shakeTime = 1.0f;
	public int shakeFrequency = 30;

	private CameraFollow followScript;
	private CameraShake shakeScript;

    // Use this for initialization
    private void Start()
    {

		followScript = new CameraFollow(gameObject.transform, target, damping, lookAheadFactor, 
		                                lookAheadReturnSpeed, lookAheadMoveThreshold);
		shakeScript = new CameraShake(shakeFactor, shakeTime, shakeFrequency);	
    }

	public void Shake()
	{
		shakeScript.Shake();
	}


    // Update is called once per frame
    private void Update()
    {
		Vector3 followPos = followScript.UpdatePosition();
		Vector3 offset = shakeScript.GetOffset();
		transform.position = (followPos + offset);
    }


}
