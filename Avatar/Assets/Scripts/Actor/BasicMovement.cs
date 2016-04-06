using System;
using UnityEngine;

namespace Assets.Scripts.Actor.Player
{
    class BasicMovement : MonoBehaviour, IMovement
    {
        public float movementSpeed = 5;
        private Rigidbody2D actor;

        private void Awake()
        {
            actor = this.gameObject.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
        }

        public void Move(Vector2 direction, float velocityPercent)
        {
            if (actor == null || !this.enabled)
                return;

            float magnitude = Mathf.Clamp(velocityPercent, 0.0f, 1.0f);
            actor.velocity = (direction.normalized * (movementSpeed * magnitude));

        }

        private void SetVelocity(Vector2 velocity)
        {
            /*
            float speed = velocity.magnitude;
            if(speed == 0.0f && slowTimer < DURATION)
            {
                physicsbody.velocity = Vector2.ClampMagnitude(physicsbody.velocity, moveSpeed);
                startVelocity = (slowTimer == 0.0f) ? physicsbody.velocity : startVelocity;
                physicsbody.velocity = Vector2.Lerp(startVelocity, Vector2.zero, (slowTimer / DURATION));
                slowTimer += Time.deltaTime;
            }
            else
            {
                slowTimer = 0.0f;
                physicsbody.velocity = velocity;

                if(speed > 0.0f)
                {
                    float angle = Mathf.Atan2 (velocity.y, velocity.x);
                    angle = MathUtils.WrapAngle (angle, 0.0f, 2.0f * Mathf.PI);
                    float angleDegree = angle * Mathf.Rad2Deg;
                    playerSprite.transform.rotation = 
                        Quaternion.AngleAxis(angleDegree, Vector3.forward);
                }
            } */
        }
    }
}
