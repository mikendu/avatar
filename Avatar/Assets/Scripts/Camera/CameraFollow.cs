using UnityEngine;

public class CameraFollow
{
	private Transform camera;
	private Transform target;
	private float damping;
	private float lookAheadFactor;
	private float lookAheadReturnSpeed;
	private float lookAheadMoveThreshold;
	
	private float m_OffsetZ;
	private Vector3 m_LastTargetPosition;
	private Vector3 m_CurrentVelocity;
	private Vector3 m_LookAheadPos;

	public CameraFollow(Transform camera, Transform target, float damping, 
	                    float lookAhead,
	                 float returnSpeed, float moveThreshold)
	{
		this.camera = camera;
		this.target = target;
		this.damping = damping;
		this.lookAheadFactor = lookAhead;
		this.lookAheadReturnSpeed = returnSpeed;
		this.lookAheadMoveThreshold = moveThreshold;


		m_LastTargetPosition = target.position;
		m_OffsetZ = (camera.position - target.position).z;
		camera.parent = null;
	}


	public Vector3 UpdatePosition()
	{
		// only update lookahead pos if accelerating or changed direction
		float xMoveDelta = (target.position - m_LastTargetPosition).x;
		bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

		// Calculate look ahead if necessary
		if (updateLookAheadTarget)
			m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
		else
			m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
		
		Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
		Vector3 newPos = Vector3.SmoothDamp(camera.position, aheadTargetPos, ref m_CurrentVelocity, damping);
		m_LastTargetPosition = target.position;

		return newPos;

	}
	
	
	
}