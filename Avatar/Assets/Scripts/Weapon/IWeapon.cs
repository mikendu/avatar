using UnityEngine;

namespace Assets.Scripts.Actor
{
    public interface IWeapon
    {
        void Begin(Vector2 point);
        void Target(Vector2 direction);
        void Execute(Vector2 direction);
        void Reset();

    }
}
