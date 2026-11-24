using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs; // Masukkan prefab mobil urut sesuai urutan di Shop
    public Transform spawnPoint;

    void Awake() // Gunakan Awake agar muncul sebelum script lain jalan
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedCarIndex", 0);

        if (selectedIndex < carPrefabs.Length)
        {
            Instantiate(carPrefabs[selectedIndex], spawnPoint.position, spawnPoint.rotation);
        }
    }
}