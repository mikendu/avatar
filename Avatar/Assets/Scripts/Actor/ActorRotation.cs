using System;
using UnityEngine;

namespace Assets.Scripts.Actor.Player
{
    class ActorRotation : MonoBehaviour, IRotateable
    {
        private void Awake()
        {
        }

        public void Rotate(float angle)
        {
            float angleDegree = angle * Mathf.Rad2Deg;
            this.transform.localRotation =
                Quaternion.AngleAxis(angleDegree, Vector3.forward);
        }

        public void Rotate(Vector2 direction)
        {
            // Calculate angle from joystick drag direction
            float angle = Mathf.Atan2(direction.y, direction.x);
            angle = MathUtils.WrapAngle(angle, 0.0f, 2.0f * Mathf.PI);
            Rotate(angle);
        }
    }
}
