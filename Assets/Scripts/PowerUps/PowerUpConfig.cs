using UnityEngine;

[System.Serializable]
public class PowerUpConfig
{
    public GameObject prefab;         // Prefab del power-up
    [Range(0f, 1f)]
    public float probability = 0.2f;  // 20% probabilidad

    public float minCooldown = 5f;    // Tiempo mínimo entre apariciones
    public float maxCooldown = 10f;   // Tiempo máximo entre apariciones

    [HideInInspector]
    public float nextSpawnTime = 0f;  // Control interno
}

