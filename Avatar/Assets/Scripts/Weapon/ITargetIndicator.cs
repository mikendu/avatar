using UnityEngine;

namespace Assets.Scripts.Actor
{
    public interface ITargetIndicator
    {
        void Hide();
        void Show();

        void SetDirection(float angle);
        void SetDirection(Vector2 direction);

    }
}
