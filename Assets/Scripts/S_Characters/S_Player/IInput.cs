using UnityEngine;

namespace S_Characters.S_Player
{
    public interface IInput
    {
        void OnEnable();
        void OnDisable();
        Vector2 GetPlayerMovement();
    }
}