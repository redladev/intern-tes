using UnityEngine;
using UnityEngine.InputSystem;

namespace Ariverse.Internship.StealthAI.Player
{
    public class CameraController : MonoBehaviour
    {
        [Header("Target & Posisi")]
        public Transform target; // Tarik objek Player lu ke sini
        public Vector3 offset = new Vector3(0f, 10f, -10f); // Jarak dan tinggi kamera dari player

        [Header("Pengaturan Mouse")]
        public float rotationSpeed = 15f; // Kecepatan muter pas klik kanan

        private float _currentRotationAngle = 0f;

        // Pake LateUpdate biar kameranya ga getar (jitter) pas ngikutin player
        private void LateUpdate()
        {
            if (target == null) return;
            if (Mouse.current == null) return;

            // Kalau Klik Kanan ditahan, baca geseran mouse ke kiri/kanan
            if (Mouse.current.rightButton.isPressed)
            {
                float mouseX = Mouse.current.delta.x.ReadValue();
                _currentRotationAngle += mouseX * rotationSpeed * Time.deltaTime;
            }

            // Hitung rotasi baru
            Quaternion rotation = Quaternion.Euler(0f, _currentRotationAngle, 0f);

            // Terapin rotasi ke offset posisi
            Vector3 rotatedOffset = rotation * offset;

            // Pindahin kamera ke atas player + offset yang udah diputer
            transform.position = target.position + rotatedOffset;

            // Bikin kamera selalu nunduk ngeliat ke arah player
            transform.LookAt(target.position);
        }
    }
}