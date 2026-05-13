using UnityEngine;

namespace Ariverse.Internship.StealthAI.Guard
{
    public class IdleState : IGuardState
    {
        private GuardController _guard;
        private float _waitTimer;

        public IdleState(GuardController guard) { _guard = guard; }

        public void EnterState()
        {
            _guard.agent.isStopped = true;
            _waitTimer = 0f;
        }

        public void UpdateState()
        {
            // Kalau Guard denger suara langkah, langsung ubah state nyari!
            if (_guard.CanHearPlayer())
            {
                _guard.ChangeState(_guard.SearchingState);
                return;
            }

            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _guard.guardData.waitTimeAtWaypoint)
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