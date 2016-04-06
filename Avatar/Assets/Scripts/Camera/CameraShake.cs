using UnityEngine;
using System;

public class CameraShake
{

	private float shakeFactor;
	private float shakeTime;
	private int shakeFrequency;

	private float shakeTimer;
	private bool shaking;
	private Vector3 shakeOffset;
	private int shakeCount;
	
	private System.Random rand;


	public CameraShake(float factor, float time, int frequency)
	{
		this.shakeFactor = factor;
		this.shakeTime = time;
		this.shakeFrequency = frequency;

		rand = new System.Random();
		shaking = false;
		reset();
	}

	public void Shake()
	{
		reset();
		shaking = true;
	}
	
	private void reset()
	{
		shakeCount = 0;
		shakeTimer = 0.0f;
		shakeOffset = Vector3.zero;
	}

	public Vector3 GetOffset()
	{
		if(shaking)
		{
			int currentCount = (int)Math.Floor(shakeTimer / (1.0f / shakeFrequency));
			if(currentCount > shakeCount)
			{
				float factor = (float)Math.Pow((shakeTimer / shakeTime), 2.0f);
				float amount = Math.Max(1.0f - factor, 0.0f) * shakeFactor;
				float xShake = (float)((rand.NextDouble () - 0.5) * amount);
				float yShake = (float)((rand.NextDouble() - 0.5) * amount);
				
				shakeOffset = (new Vector3(xShake, yShake, 0) * Time.deltaTime);
				shakeCount = currentCount;
			}
			
			shakeTimer += Time.deltaTime;
			
			if(shakeTimer > shakeTime)
			{
				shaking = false;
				reset();
			}
		}
		
		return shakeOffset;
	}
}