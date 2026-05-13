using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ariverse.Internship.StealthAI.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        // Pake Vector2 sekarang, biar lebih gampang ngitung X dan Z nya nanti
        public event Action<Vector2> OnMoveInput;
        public event Action<bool> OnSneakInput;

        private void Update()
        {
            if (Keyboard.current == null) return;

            float x = 0f;
            float z = 0f;

            // Baca murni dari hardware keyboard
            if (Keyboard.current.dKey.isPressed) x = 1f;
            if (Keyboard.current.aKey.isPressed) x = -1f;
            if (Keyboard.current.wKey.isPressed) z = 1f;
            if (Keyboard.current.sKey.isPressed) z = -1f;

            // Kirim input raw-nya
            Vector2 rawInput = new Vector2(x, z).normalized;
            OnMoveInput?.Invoke(rawInput);

            // Sneak Input
            if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
            {
                OnSneakInput?.Invoke(true);
            }
            else if (Keyboard.current.leftShiftKey.wasReleasedThisFrame)
            {
                OnSneakInput?.Invoke(false);
            }
        }
    }
}