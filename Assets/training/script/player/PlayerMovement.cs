using UnityEngine;

namespace Ariverse.Internship.StealthAI.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerInputHandler))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerDataSO playerData;

        [Header("Camera Reference")]
        [Tooltip("Masukin Main Camera ke sini biar WASD ngikutin arah layar")]
        public Transform cameraTransform;

        private Rigidbody _rb;
        private PlayerInputHandler _inputHandler;

        private Vector2 _currentMoveInput;
        private bool _isSneaking;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _inputHandler = GetComponent<PlayerInputHandler>();

            if (cameraTransform == null && Camera.main != null)
            {
                cameraTransform = Camera.main.transform;
            }
        }

        private void OnEnable()
        {
            _inputHandler.OnMoveInput += UpdateMoveInput;
            _inputHandler.OnSneakInput += UpdateSneakState;
        }

        private void OnDisable()
        {
            _inputHandler.OnMoveInput -= UpdateMoveInput;
            _inputHandler.OnSneakInput -= UpdateSneakState;
        }

        private void UpdateMoveInput(Vector2 input)
        {
            _currentMoveInput = input;
        }

        private void UpdateSneakState(bool isSneaking)
        {
            _isSneaking = isSneaking;
        }

        private void FixedUpdate()
        {
            if (playerData == null) return;
            if (cameraTransform == null) return;

            float currentSpeed = _isSneaking ? playerData.GetSneakSpeed() : playerData.baseWalkSpeed;

            // --- LOGIKA KAMERA RELATIF ---
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            // currentMoveInput.y = input W/S, currentMoveInput.x = input A/D
            Vector3 moveDirection = (camForward * _currentMoveInput.y + camRight * _currentMoveInput.x).normalized;

            // Hajar velocity
            Vector3 newVelocity = moveDirection * currentSpeed;
            newVelocity.y = _rb.linearVelocity.y; // Biar tetep bisa jatuh kena gravitasi
            _rb.linearVelocity = newVelocity;

            // Lirik / Rotasi
            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                _rb.rotation = Quaternion.RotateTowards(_rb.rotation, toRotation, 720f * Time.fixedDeltaTime);
            }
        }
    }
}