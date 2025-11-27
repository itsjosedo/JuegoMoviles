using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpController : MonoBehaviour
{
    // Aquí guardamos los power ups activos 
    private Dictionary<PowerUp, Coroutine> activePowerUps = new Dictionary<PowerUp, Coroutine>();

    //  Ejemplo de stats del jugador (ajusta a tu propio PlayerController)
    [Header("Player Stats")]
    public float moveSpeed = 5f;
    public float fireRate = 0.25f;
    public int maxHealth = 5;
    public int currentHealth;
    

    //variables escudo
    public bool escudoActivo = false;
    public int hitsEscudo = 0;
    public float tiempoEscudo = 0;
    public GameObject efectoEscudo;

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

    public void IncreaseFireRate(float amount)
    {
        fireRate -= amount;
        fireRate = Mathf.Max(0.05f, fireRate); // evita valores negativos
    }

    public void ResetFireRate(float amount)
    {
        fireRate += amount;
    }

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
    // Aquí puedes añadir más según tus necesidades:
    // - doble daño
    // - escudo
    // - tamaño del player
    // - triple disparo
    // - magneto para recoger items
}
