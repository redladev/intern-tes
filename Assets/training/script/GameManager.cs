using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Wajib buat fitur Restart
using UnityEngine.InputSystem;     // Wajib buat baca tombol R

namespace Ariverse.Internship.StealthAI
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public TextMeshProUGUI statusText;
        private bool _isGameOver = false;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            
            Time.timeScale = 1f;
        }

        private void Update()
        {
            // Kalau udah Game Over, tungguin input tombol 'R' buat Restart
            if (_isGameOver && Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            {
                RestartLevel();
            }

            // --- TAMBAHIN INI BIAR BISA KELUAR GAME ---
            if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Application.Quit();

                Debug.Log("Keluar dari Game!");
            }
        }

        public void GameOverCaught()
        {
            statusText.text = "CAUGHT BY GUARD!\n<size=50%>Press 'R' to Restart</size>";
            statusText.color = Color.red;
            Time.timeScale = 0f;
            _isGameOver = true;
        }

        public void GameWinEscaped()
        {
            statusText.text = "MISSION ACCOMPLISHED!\n<size=50%>Press 'R' to Restart</size>";
            statusText.color = Color.green;
            Time.timeScale = 0f;
            _isGameOver = true;
        }

        private void RestartLevel()
        {
            // Muat ulang Scene yang lagi dimainin sekarang
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}