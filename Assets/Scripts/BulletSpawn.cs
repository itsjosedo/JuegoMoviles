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
    
    }

    IEnumerator spawnBullets()
    {
        PlayerPowerUpController controller = FindObjectOfType<PlayerPowerUpController>();

        while (true)
        {
            foreach (Transform spawns in bulletsSpawn)
            {
                GameObject newBullet = Instantiate(bullet, spawns.position, spawns.rotation);
                Bullet bulletScript = newBullet.GetComponent<Bullet>();

                if (bulletScript != null)
                {
                    bulletScript.SetDirection(spawns.forward);
                }
                else
                {
                    bulletScript.SetDirection(new Vector3(0,0,-1));
                }
            }
            float wait = spawnTimeBullet;
            if (controller != null)
            {
                // Si fireRateMultiplier = 2 => dispara el doble de rápido (espera la mitad)
                wait = spawnTimeBullet / controller.spawnMultiplicador;
            }

            yield return new WaitForSeconds(wait);
        }
    }
}
