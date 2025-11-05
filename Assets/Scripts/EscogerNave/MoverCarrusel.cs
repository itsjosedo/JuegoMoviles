using System.Collections;
using UnityEngine;

public class MoverCarrusel : MonoBehaviour
{
    //[SerializeField] private GameObject carrusel;
    private float timeToWait = 2;
    private Vector3 posIzq = new Vector3(525f, 519f, 251.6f);
    private Vector3 posDer = new Vector3(535f, 519f, 251.6f);

    public void MoverCarruselDerecha()
    {
        StartCoroutine(CMoverCarruselDerecha());
    }
    public void MoverCarruselIzquierda()
    {
        StartCoroutine(CMoverCarruselIzquierda());
    }
    IEnumerator CMoverCarruselDerecha()
    {
        float time = 0;
        while(time < timeToWait)
        {
            transform.position = Vector3.Lerp(posDer, posIzq, time / timeToWait);
            time += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator CMoverCarruselIzquierda()
    {
        float time = 0;
        while (time < timeToWait)
        {
            transform.position = Vector3.Lerp( posIzq, posDer, time / timeToWait);
            time += Time.deltaTime;
            yield return null;
        }
    }


}
