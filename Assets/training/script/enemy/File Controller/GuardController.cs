using UnityEngine;
using UnityEngine.AI;

namespace Ariverse.Internship.StealthAI.Guard
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class GuardController : MonoBehaviour
    {
        public GuardDataSO guardData;
        public Transform[] waypoints;

        [Header("Vision Targets & Layers")]
        public Transform playerTarget;
        public LayerMask obstacleMask;
        public LayerMask playerMask;

        [HideInInspector] public NavMeshAgent agent;
        [HideInInspector] public int currentWaypointIndex = 0;
        [HideInInspector] public Vector3 lastKnownPosition;

        private IGuardState _currentState;

        public IdleState IdleState { get; private set; }
        public PatrolState PatrolState { get; private set; }
        public SuspiciousState SuspiciousState { get; private set; }
        public AlertState AlertState { get; private set; }
        public SearchingState SearchingState { get; private set; }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();

            IdleState = new IdleState(this);
            PatrolState = new PatrolState(this);
            SuspiciousState = new SuspiciousState(this);
            AlertState = new AlertState(this);
            SearchingState = new SearchingState(this);
        }

        private void Start() => ChangeState(PatrolState);

        private void Update()
        {
            _currentState?.UpdateState();

            // Kalau kelihatan player, langsung masuk Alert State
            if (CanSeePlayer() && _currentState != AlertState)
            {
                ChangeState(AlertState);
            }
        }

        public void ChangeState(IGuardState newState)
        {
            _currentState?.ExitState();
            _currentState = newState;
            _currentState?.EnterState();
        }

        // --- FUNGSI MATA (Tadi sempet kehapus sama lu wkwk) ---
        public bool CanSeePlayer()
        {
            if (playerTarget == null) return false;

            Vector3 dirToPlayer = (playerTarget.position - transform.position).normalized;
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

            if (distanceToPlayer < guardData.visionRadius)
            {
                float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);
                if (angleToPlayer < guardData.visionAngle / 2f)
                {
                    // Raycast biar nggak nembus tembok
                    if (!Physics.Raycast(transform.position, dirToPlayer, distanceToPlayer, obstacleMask))
                    {
                        lastKnownPosition = playerTarget.position;
                        return true;
                    }
                }
            }
            return false;
        }

        // --- FUNGSI KUPING ---
        public bool CanHearPlayer()
        {
            if (playerTarget == null) return false;

            // Cek jarak antara Guard dan Player
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

            // Kalau masuk radius pendengaran
            if (distanceToPlayer < guardData.hearingRadius)
            {
                // Ambil Rigidbody player buat ngecek dia lagi gerak kenceng atau ngendap-ngendap
                Rigidbody playerRb = playerTarget.GetComponent<Rigidbody>();
                if (playerRb != null)
                {
                    // Angka 2.6f ini diambil dari sneakSpeed (2.5f). 
                    // Kalau kecepatannya di atas 2.5, berarti dia lagi lari (bikin suara)!
                    if (playerRb.linearVelocity.magnitude > 2.6f)
                    {
                        lastKnownPosition = playerTarget.position; // Ingetin lokasi suaranya
                        return true;
                    }
                }
            }
            return false;
        }

        // --- VISUALISASI RADIUS & CONE (GIZMOS) ---
        private void OnDrawGizmosSelected()
        {
            if (guardData == null) return;

            // 1. Gambar Lingkaran Radius Jarak (Warna Kuning)
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, guardData.visionRadius);

            // 2. Gambar Sudut Pandang / Vision Cone (Warna Merah)
            Gizmos.color = Color.red;

            // Hitung sudut kiri dan kanan berdasarkan posisi karakter ngadep mana
            Vector3 leftBoundary = Quaternion.Euler(0, -guardData.visionAngle / 2f, 0) * transform.forward;
            Vector3 rightBoundary = Quaternion.Euler(0, guardData.visionAngle / 2f, 0) * transform.forward;

            // Gambar garisnya dari badan AI ke ujung radius
            Gizmos.DrawLine(transform.position, transform.position + leftBoundary * guardData.visionRadius);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary * guardData.visionRadius);

            // 3. Gambar Radius Pendengaran (Warna Biru)
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, guardData.hearingRadius);
        }
    }
}