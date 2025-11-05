using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BulletSpawn : MonoBehaviour
{
    //public GameObject bulletSpawn;
    public Transform[] bulletsSpawn;
    public GameObject bullet;
    public float spawnTimeBullet;
    private IEnumerator bulletCoroutine;
    
    void Start()
    {
        bulletCoroutine = spawnBullets();
        StartCoroutine(bulletCoroutine);
    }



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(bulletCoroutine);
        }
    }

    IEnumerator spawnBullets()
    {
        while(true)
        {
            foreach (Transform spawns in bulletsSpawn)
            {
                GameObject newBullet = Instantiate(bullet, spawns.position, spawns.rotation);
                Bullet bulletScript = newBullet.GetComponent<Bullet>();

                if (bulletScript != null)
                    bulletScript.SetDirection(spawns.forward);
            }
            yield return new WaitForSeconds(spawnTimeBullet);
        }
    }
}
