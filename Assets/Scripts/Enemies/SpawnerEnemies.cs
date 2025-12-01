using UnityEngine;

public class SpawnerEnemies : MonoBehaviour
{
    [SerializeField] private GameObject NaveEnemiga;
    [SerializeField] private float timeSpawnSelected = 2f;
    private float timeSpawn;
    private float randomPosition;
    private Vector3 positionVector;

    
    

    void Start()
    {
        positionVector = new Vector3(randomPosition,0,7.18f);
    }

    // Update is called once per frame
    void Update()
    {
        timeSpawn -= Time.deltaTime;
        if( timeSpawn < 0)
        {
            randomPosition = Random.Range(2.5f , 2.2f);

            Instantiate(NaveEnemiga, positionVector, Quaternion.identity);
            timeSpawn = timeSpawnSelected;
        }
    }
}
