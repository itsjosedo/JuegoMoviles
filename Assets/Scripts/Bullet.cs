using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifeTime = 5f;
    public float damage = 1f;
    private PlayerPowerUpController playerController;

    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        
        direction = dir.normalized;
        
        Destroy(gameObject, lifeTime);
    }
    private void Start()
    {
        playerController = Object.FindAnyObjectByType<PlayerPowerUpController>();
        //playerController = FindObjectOfType<PlayerPowerUpController>();
    }

    void Update()
    {
        float finalSpeed = speed;
        if (playerController != null)
            finalSpeed = speed * playerController.bulletSpeedMultiplicador;

        transform.Translate(direction * finalSpeed * Time.deltaTime, Space.World);
       // Debug.Log(transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Si la bala debe dañar al Player o enemigos, multiplica el daño por el multiplicador
        float finalDamage = damage;
        if (playerController != null)
            finalDamage = damage * playerController.bulletDamageMultiplicador;
        
        // Ejemplo: si impacta a un enemigo (tag "Enemy"), aplicar daño ahí
        if (other.CompareTag("Enemy"))
        {
            
            Destroy(other);
            Destroy(gameObject);
        }

        // Si la bala choca con el player (si enemigos usan estas balas), aplica igual
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TomarDaño(1); // si tu TomarDaño recibe int
            }
            Destroy(gameObject);
        }
    }

}

