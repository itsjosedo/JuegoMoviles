using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    private double minX, maxX;
    [SerializeField] private float speedX = 3f;
    [SerializeField] private float speedZ = 3f;


    private float direction = 1f;


    void Start()
    {
        float dist = Mathf.Abs(Camera.main.transform.position.y - transform.position.y);

        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        //Debug.Log("minX: " + minX + ", maxX: " + maxX);
        minX *= 0.9;
        maxX *= 0.9;
        //Debug.Log("minX: " + minX + ", maxX: " + maxX);
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * speedX * direction * Time.deltaTime;
        transform.Translate(new Vector3(0,0,-1) * speedZ * Time.deltaTime);

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
