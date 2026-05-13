using UnityEngine;

namespace Ariverse.Internship.StealthAI.Player
{
    [CreateAssetMenu(fileName = "NewPlayerData", menuName = "Ariverse/StealthAI/PlayerData")]
    public class PlayerDataSO : ScriptableObject
    {
        [Header("Movement Settings")]
        [Tooltip("Kecepatan jalan normal player")]
        public float baseWalkSpeed = 5f;

        [Tooltip("Pengali kecepatan saat player melakukan sneak/crouch")]
        public float sneakSpeedModifier = 0.5f;

        // Fungsi bantuan untuk mendapatkan kecepatan sneak
        public float GetSneakSpeed() => baseWalkSpeed * sneakSpeedModifier;
    }
}