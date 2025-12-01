using UnityEngine;

public class DestroyEnemies : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            if (GameManager.Instance != null) GameManager.Instance.AddScore(50);
        }
    }
}
