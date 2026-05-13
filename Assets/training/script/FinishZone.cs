using UnityEngine;

namespace Ariverse.Internship.StealthAI
{
    public class FinishZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // Pastiin objek Player lu udah dikasih Tag "Player" di Inspector
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.GameWinEscaped();
            }
        }
    }
}