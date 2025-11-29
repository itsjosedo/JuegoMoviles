using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpController : MonoBehaviour
{
    // Aquí guardamos los power ups activos 
    private Dictionary<PowerUp, Coroutine> activePowerUps = new Dictionary<PowerUp, Coroutine>();

    //stats del jugador 


    [Header("Player Stats")]
    [HideInInspector] public float moveSpeed = 5f;
    [HideInInspector] public int maxHealth = 5;
    [HideInInspector] public int currentHealth;
    

    //variables escudo
    [HideInInspector]public bool escudoActivo = false;
    [HideInInspector] public int hitsEscudo = 0;
    [HideInInspector] public float tiempoEscudo = 0;
    public GameObject efectoEscudo;



    //Stats del disparo
    public float bulletSpeed = 15f; //Solo sirve como refrencia
    public float bulletSpeedMultiplicador = 1f;

    public float spawnBase = 1f; //Solo sirve como refrencia
    public float spawnMultiplicador = 1f;

    public float bulletDamage = 1f; //Solo sirve como refrencia
    public float bulletDamageMultiplicador = 1f;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if (escudoActivo)
        {
            tiempoEscudo -= Time.deltaTime;

            if (tiempoEscudo <= 0)
            {
                DesactivarEscudo();
            }
        }

    }

    // -------------------------------------------------------------
    //        MÉTODO CENTRAL LLAMADO POR LOS POWER UPS
    // -------------------------------------------------------------
    public IEnumerator HandleTemporalPowerUp(PowerUp powerUp, float duration)
    {
        // Si ya existe ese power up activo, lo reinicia
        if (activePowerUps.ContainsKey(powerUp))
        {
            StopCoroutine(activePowerUps[powerUp]);
            activePowerUps.Remove(powerUp);
        }

        // Inicia la duración del power up
        Coroutine routine = StartCoroutine(PowerUpTimer(powerUp, duration));
        activePowerUps.Add(powerUp, routine);

        yield break;
    }

    private IEnumerator PowerUpTimer(PowerUp powerUp, float duration)
    {
        yield return new WaitForSeconds(duration);

        // Llama al Remove solo si existe en el powerUp
        powerUp.Remove(this);

        // Lo saca de la lista de activos
        if (activePowerUps.ContainsKey(powerUp))
            activePowerUps.Remove(powerUp);
    }

    // -------------------------------------------------------------
    //        MÉTODOS DE UTILIDAD PARA TUS POWER UPS
    // -------------------------------------------------------------

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public void IncreaseMoveSpeed(float amount)
    {
        moveSpeed += amount;
    }

    public void ResetMoveSpeed(float amount)
    {
        moveSpeed -= amount;
    }
    
    //Metodo para activar escudo
    public void ActivarEscudo(float tiempo)
    {
        escudoActivo = true;
        tiempoEscudo = tiempo;

        if(efectoEscudo != null)
        {
            efectoEscudo.SetActive(true);
        }
    }

    public void DesactivarEscudo()
    {
        escudoActivo = false;
        tiempoEscudo = 0;

        if(efectoEscudo != null)
        {
            efectoEscudo.SetActive(false);
        }
    }

    //metodos para activar y desactivar el spawn de las balas.
    public void AddSpawnMultiplicador(float amount)
    {
        spawnMultiplicador += amount;
    }
    public void RemoveSpawnMultiplicador(float amount)
    {
        spawnMultiplicador -= amount;
        if(spawnMultiplicador < 0.1f)
        {
            spawnMultiplicador = 1;
        }
    }
    
    public void AddBulletSpeed(float amount)
    {
        bulletSpeedMultiplicador += amount; 
    }
    public void RemoveBulletSpeed(float amount)
    {
        bulletSpeedMultiplicador -= amount;
        if (bulletSpeedMultiplicador < 0.05f)
        {
            bulletSpeedMultiplicador = 0.05f;
        }

        
    }
    public void AddBulletDamageMultiplicador(float amount)
    {
        bulletDamageMultiplicador += amount;
    }

    public void RemoveBulletDamageMultiplicador(float amount)
    {
        bulletDamageMultiplicador -= amount;
        if (bulletDamageMultiplicador < 0.1f)
        {
            bulletDamageMultiplicador = 0.1f;
        }
    }
}
