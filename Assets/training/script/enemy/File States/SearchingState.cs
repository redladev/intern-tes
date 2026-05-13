using UnityEngine;

namespace Ariverse.Internship.StealthAI.Guard
{
    public class SearchingState : IGuardState
    {
        private GuardController _guard;

        public SearchingState(GuardController guard) { _guard = guard; }

        public void EnterState()
        {
            _guard.agent.SetDestination(_guard.lastKnownPosition);
        }

        public void UpdateState()
        {
            if (!_guard.agent.pathPending && _guard.agent.remainingDistance <= _guard.agent.stoppingDistance)
            {
                _guard.ChangeState(_guard.SuspiciousState);
            }
        }

        public void ExitState() { }
    }
}