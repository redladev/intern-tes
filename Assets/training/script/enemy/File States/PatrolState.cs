using UnityEngine;

namespace Ariverse.Internship.StealthAI.Guard
{
    public class PatrolState : IGuardState
    {
        private GuardController _guard;

        public PatrolState(GuardController guard) { _guard = guard; }

        public void EnterState()
        {
            _guard.agent.speed = _guard.guardData.patrolSpeed;
            MoveToNextWaypoint();
        }

        public void UpdateState()
        {
            // Kalau Guard denger suara langkah, langsung ubah state nyari!
            if (_guard.CanHearPlayer())
            {
                _guard.ChangeState(_guard.SearchingState);
                return;
            }

            if (!_guard.agent.pathPending && _guard.agent.remainingDistance <= _guard.agent.stoppingDistance)
            {
                if (_guard.waypoints.Length > 0)
                {
                    _guard.ChangeState(_guard.IdleState);
                }
            }
        }

        public void ExitState()
        {
            _guard.agent.ResetPath();
        }

        private void MoveToNextWaypoint()
        {
            if (_guard.waypoints.Length == 0) return;
            _guard.agent.SetDestination(_guard.waypoints[_guard.currentWaypointIndex].position);
            _guard.currentWaypointIndex = (_guard.currentWaypointIndex + 1) % _guard.waypoints.Length;
        }
    }
}