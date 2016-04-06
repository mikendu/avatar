using UnityEngine;

namespace Assets.Scripts.Actor
{
   
    public interface IRotateable
    {
        
        void Rotate(float angle);
        void Rotate(Vector2 direction);
    }
}
