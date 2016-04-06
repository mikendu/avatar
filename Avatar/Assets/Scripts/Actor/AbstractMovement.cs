using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Actor.Player
{
    public abstract class AbstractMovement : MonoBehaviour, IMovement
    {
        public float movementSpeed = 5;
        protected Dictionary<MovementFactor, bool> factorMap = new Dictionary<MovementFactor, bool>();

        public virtual void Move(Vector2 direction, float velocityPercent)
        {
            throw new NotImplementedException();
        }

        public virtual void NotifyFactor(MovementFactor factor, bool value)
        {
            this.factorMap[factor] = value;
            evaluateFactors();
        }

        protected bool getFactor(MovementFactor factor)
        {
            bool value = false;
            if (factorMap.ContainsKey(factor))
                value = factorMap[factor];

            return value;
        }

        protected abstract void evaluateFactors();
    }
}
