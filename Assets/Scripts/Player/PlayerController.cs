using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private PlayerPowerUpController powerUpController;

    private void Start()
    {
        powerUpController = GetComponent<PlayerPowerUpController>();
    }

    public void TomarDaño(int cantidad)
    {
        if (powerUpController.escudoActivo)
        {
            Debug.Log("Golpe BLOQUEADO por escudo");
            return; // NO recibe daño
        }

        GameManager.Instance.LoseLife();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Golpe recibido SIN escudo");
            TomarDaño(1);
            //Destroy(other.gameObject);
        }
    }
}
