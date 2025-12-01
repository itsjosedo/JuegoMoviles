using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    public float speed = 15f;
    public float lifeTime = 5f;
    public float damage = 1f;
    private PlayerPowerUpController playerController;

    private double minX, maxX;
    [SerializeField] private float speedX = 3f;
    [SerializeField] private float speedZ = 3f;


    private float direction = 1f;



    void Start()
    {
        playerController = Object.FindAnyObjectByType<PlayerPowerUpController>();
        MovementVariablesInitialization();
    }

    // Update is called once per frame
    void Update()
    {
        MovementBull();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Si la bala debe dañar al Player o enemigos, multiplica el daño por el multiplicador
        float finalDamage = damage;
        if (playerController != null)
            finalDamage = damage * playerController.bulletDamageMultiplicador;

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
    private void MovementVariablesInitialization()
    {
        direction = Random.Range(0, 2) == 0 ? -1 : 1;

        float dist = Mathf.Abs(Camera.main.transform.position.y - transform.position.y);

        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        //Debug.Log("minX: " + minX + ", maxX: " + maxX);
        minX *= 0.9;
        maxX *= 0.9;
        //Debug.Log("minX: " + minX + ", maxX: " + maxX);
    }

    private void MovementBull()
    {
        transform.position += Vector3.right * speedX * direction * Time.deltaTime;
        transform.Translate(new Vector3(0, 0,  1) * speedZ * Time.deltaTime);

        if (transform.position.x >= maxX)
        {
            direction = -1f;
        }

        if (transform.position.x <= minX)
        {
            direction = 1f;
        }
    }


}
