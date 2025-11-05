using UnityEngine;

public class NaveSeleccionada : MonoBehaviour
{
    //Este escript se usa en la Escena de Game para poner en uso la nave previamente seleccionada
    
    [SerializeField] private GameObject[] naves;
    int naveSeleccionada; 

    void Start()
    {
        naveSeleccionada = EscogerNave.NAVE;
        naves[naveSeleccionada].SetActive(true);
        Debug.Log("La nave es: "+ EscogerNave.NAVE);
    }

    
    
}
