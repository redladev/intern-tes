using UnityEngine;

namespace Ariverse.Internship.StealthAI.Guard
{
    public interface IGuardState
    {
        void EnterState();
        void UpdateState();
        void ExitState();
    }
}