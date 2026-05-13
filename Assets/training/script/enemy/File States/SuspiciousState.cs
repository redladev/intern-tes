using UnityEngine;

namespace Ariverse.Internship.StealthAI.Guard
{
    public class SuspiciousState : IGuardState
    {
        private GuardController _guard;
        private float _searchTimer;

        public SuspiciousState(GuardController guard) { _guard = guard; }

        public void EnterState()
        {
            _guard.agent.isStopped = true;
            _searchTimer = 0f;
        }

        public void UpdateState()
        {
            _searchTimer += Time.deltaTime;
            if (_searchTimer > 3f) // Waktu nyari sisa jejak
            {
                _guard.ChangeState(_guard.PatrolState);
            }
        }

        public void ExitState()
        {
            _guard.agent.isStopped = false;
        }
    }
}