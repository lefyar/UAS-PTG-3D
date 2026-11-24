using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private float rotateSpeed = 100f;

    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ScoreSystem.Instance != null)
            {
                ScoreSystem.Instance.AddScore(scoreValue);
            }

            Destroy(gameObject);
        }
    }
}