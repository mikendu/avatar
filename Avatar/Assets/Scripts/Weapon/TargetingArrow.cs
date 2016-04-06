using UnityEngine;

namespace Assets.Scripts.Actor
{
    class TargetingArrow : MonoBehaviour, ITargetIndicator
    {
        private SpriteRenderer spriteRenderer;
        public float arrowDistance = 10.0f;

        void Awake()
        {
            this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
            SetDirection(new Vector2(1, 0));
        }

        public void Hide()
        {
            spriteRenderer.enabled = false;
        }

        public void Show()
        {
            spriteRenderer.enabled = true;
        }

        public void SetDirection(float angle)
        {
            setArrowRotation(angle);

            float x = arrowDistance * Mathf.Cos(angle);
            float y = arrowDistance * Mathf.Sin(angle);
            setArrowPosition(new Vector2(x, y));

        }

        public void SetDirection(Vector2 direction)
        {
            // Calculate angle from joystick drag direction
            float angle = Mathf.Atan2(direction.y, direction.x);
            angle = MathUtils.WrapAngle(angle, 0.0f, 2.0f * Mathf.PI);
            setArrowRotation(angle);
            setArrowPosition(arrowDistance * direction.normalized);
        }


        public void setArrowRotation(float angle)
        {
            float angleDegree = angle * Mathf.Rad2Deg;
            this.transform.rotation =
                Quaternion.AngleAxis(angleDegree, Vector3.forward);
        }

        private void setArrowPosition(Vector2 position)
        {
            bool nullParent = (transform.parent == null);
            Vector3 parentPos = nullParent ? Vector3.zero : transform.parent.position;
            Vector3 parentScale = nullParent ? Vector3.one : transform.parent.lossyScale;
            Vector3 offset = new Vector3(position.x, position.y);           

            this.transform.position = parentPos + Vector3.Scale(parentScale, offset);

        }

    }
}
