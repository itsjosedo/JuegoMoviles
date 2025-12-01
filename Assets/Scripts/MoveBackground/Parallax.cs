using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed;

    private float spriteHeight;
    private Vector3 startPos;   
    void Start()
    {
        startPos = transform.position;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.z;
        //Debug.Log("altura: " + spriteHeight + ", Suma: " + (startPos.z - spriteHeight));
    }

    void Update()
    {
        transform.Translate(Vector3.down * parallaxSpeed * Time.deltaTime);
        if(transform.position.z < startPos.z - spriteHeight)
        {
            transform.position = startPos; 
        }
    }
}
