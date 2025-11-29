using UnityEngine;
using System.Collections;
using System.Linq;

public class PowerUpSpawnerPro : MonoBehaviour
{
    public Transform[] spawnPoints;          // puntos de spawn
    public PowerUpConfig[] powerUps;         // lista de powerups

    public float checkInterval = 1f;         // cada cuánto revisa si ya debe spawnear algo

    private void Start()
    {
        // Inicializar cooldown de cada power up
        foreach (var p in powerUps)
        {
            p.nextSpawnTime = Time.time + Random.Range(p.minCooldown, p.maxCooldown);
        }

        StartCoroutine(SpawnerLoop());
    }

    IEnumerator SpawnerLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            // obtener powerups listos para aparecer
            var candidates = powerUps
                .Where(p => Time.time >= p.nextSpawnTime)
                .ToList();

            if (candidates.Count == 0)
                continue;

            // Seleccionar uno basado en probabilidades
            PowerUpConfig chosen = GetPowerUpByProbability(candidates);

            if (chosen != null)
            {
                SpawnPowerUp(chosen);

                // reiniciar cooldown de ese power-up
                chosen.nextSpawnTime = Time.time + Random.Range(chosen.minCooldown, chosen.maxCooldown);
            }
        }
    }

    PowerUpConfig GetPowerUpByProbability(System.Collections.Generic.List<PowerUpConfig> list)
    {
        float totalProb = list.Sum(p => p.probability);
        float randomValue = Random.value * totalProb;
        float current = 0f;

        foreach (var p in list)
        {
            current += p.probability;
            if (randomValue <= current)
                return p;
        }
        return null;
    }

    void SpawnPowerUp(PowerUpConfig config)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(config.prefab, spawnPoint.position, Quaternion.identity);
    }
}
