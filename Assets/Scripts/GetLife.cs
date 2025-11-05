using UnityEngine;

public class GetLife : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Colision detectada: Toma una vida");
            if (GameManager.Instance != null) GameManager.Instance.AddLife(1);
        }
    }
}
