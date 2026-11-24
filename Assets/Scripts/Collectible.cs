using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10; // Ubah nilai ini di Inspector untuk 3 jenis berbeda
    [SerializeField] private GameObject pickupEffect; // Optional: Particle system saat diambil

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Pastikan mobilmu punya tag "Player"
        {
            // Panggil fungsi AddScore yang sudah kita buat tadi
            if (ScoreSystem.Instance != null)
            {
                ScoreSystem.Instance.AddScore(scoreValue);
            }

            // Visual effect (Juice)
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject); // Hapus koin
        }
    }
}