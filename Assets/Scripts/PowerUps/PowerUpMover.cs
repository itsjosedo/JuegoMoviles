using UnityEngine;

public class PowerUpMover : MonoBehaviour
{
    public float speed = 2f;
    public float lifeTime = 8f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
    }
}
