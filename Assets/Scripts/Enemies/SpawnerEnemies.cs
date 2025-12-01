using UnityEngine;

public class SpawnerEnemies : MonoBehaviour
{
    [SerializeField] private GameObject NaveEnemiga;
    [SerializeField] private float timeSpawnSelected = 2f;
    private float timeSpawn;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSpawn -= Time.deltaTime;
        if( timeSpawn < 0)
        {
            Instantiate(NaveEnemiga, transform.position, Quaternion.identity);
            timeSpawn = timeSpawnSelected;
        }
    }
}
