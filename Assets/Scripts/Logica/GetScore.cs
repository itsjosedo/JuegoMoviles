using UnityEngine;

public class GetScore : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(GameManager.Instance);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{name} OnTriggerEnter con {other.name} (tag: {other.tag})");

        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Impacto bala detectado");
            if (GameManager.Instance != null) GameManager.Instance.AddScore(50);
        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("Impacto player detectado");
            var player = GetComponentInParent<PlayerController>();
            if (player != null)
                player.TomarDaño(1);
        }
    }

}
