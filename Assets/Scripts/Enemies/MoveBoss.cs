using UnityEngine;

public class MoveBoss : MonoBehaviour
{
    [SerializeField] private float speedZ;
    [SerializeField] private GameObject enableBullets;
    [SerializeField] private GameObject disableShips;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > 2.5)
        {
            transform.Translate(new Vector3(0, 0, 1) * speedZ * Time.deltaTime);
            disableShips.SetActive(false);
        }
        else
        {
            enableBullets.SetActive(true);
        }

    }
}
