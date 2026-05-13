using UnityEngine;

namespace Ariverse.Internship.StealthAI.Guard
{
    public class AlertState : IGuardState
    {
        private GuardController _guard;

        public AlertState(GuardController guard) { _guard = guard; }

        public void EnterState()
        {
            _guard.agent.speed = _guard.guardData.chaseSpeed;
        }

        public void UpdateState()
        {
            if (_guard.CanSeePlayer())
            {
                _guard.agent.SetDestination(_guard.playerTarget.position);

                // Cek Lose Condition: Jarak terlalu dekat = ketangkep
                float dist = Vector3.Distance(_guard.transform.position, _guard.playerTarget.position);
                if (dist < 0.5f)
                {
                    // PANGGIL UI GAME OVER DI SINI!
                    GameManager.Instance.GameOverCaught();
                }
            }
            else
            {
                _guard.ChangeState(_guard.SearchingState);
            }
        }

        public void ExitState() { }
    }
}