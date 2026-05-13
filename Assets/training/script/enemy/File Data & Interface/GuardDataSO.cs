using UnityEngine;

namespace Ariverse.Internship.StealthAI.Guard
{
    [CreateAssetMenu(fileName = "NewGuardData", menuName = "Ariverse/StealthAI/GuardData")]
    public class GuardDataSO : ScriptableObject
    {
        [Header("Movement")]
        public float patrolSpeed = 2f;
        public float chaseSpeed = 4.5f;
        public float waitTimeAtWaypoint = 2f;

        [Header("Vision")]
        public float visionRadius = 8f;
        public float visionAngle = 45f;

        [Header("Hearing")]
        public float hearingRadius = 5f;
    }
}