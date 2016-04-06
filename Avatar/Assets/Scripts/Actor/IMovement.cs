using UnityEngine;

namespace Assets.Scripts.Actor
{
    public enum MovementFactor
    {
        InputAvailable,
        AbilityEngaged
    }
    
    /* 
        Interface for componenets that provide movement
        ability for actors in the game. The type of movement
        will depend on the implementation of the individual componenets,
        but they will share a common interface. 
        */
    public interface IMovement
    {
        /* Moves the actor in the given direction, where
           the percentage represents a "velocity" with which to move.
           Percentage ranges between 0 and 1, where 1 will indicate
           to move with the highest possible velocity. 
           */
        void Move(Vector2 direction, float velocityPercent);

        void NotifyFactor(MovementFactor factor, bool value);
    }
}
