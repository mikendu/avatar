using System;
using UnityEngine;

namespace Assets.Scripts.Actor.Player
{
    public class BasicMovement : AbstractMovement
    {
        private Rigidbody2D actor;

        private void Awake()
        {
            actor = this.gameObject.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
        }

        public override void Move(Vector2 direction, float velocityPercent)
        {
            if (actor == null || !this.enabled)
                return;

            float magnitude = Mathf.Clamp(velocityPercent, 0.0f, 1.0f);
            actor.velocity = (direction.normalized * (movementSpeed * magnitude));

        }

        protected override void evaluateFactors()
        {
            bool input = getFactor(MovementFactor.InputAvailable);
            bool ability = getFactor(MovementFactor.AbilityEngaged);

            this.enabled = (input && !ability);
        }
    }
}
