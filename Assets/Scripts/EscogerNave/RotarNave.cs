using UnityEngine;

public class RotarNave : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 0, 100 * Time.deltaTime);
    }
}
