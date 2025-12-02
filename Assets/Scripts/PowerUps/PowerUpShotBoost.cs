using UnityEngine;

public class PowerUpShotBoost : PowerUp
{
    [Header("Shot Boost values")]
    public float addBulletSpeedMultiplicador = 0.5f;   // +50% speed
    public float addSpawnmultiplicador = 0.5f;      // +50% fire rate (más rápido)
    public float addDamageMultiplicador = 0.5f;        // +50% damage

    // Apply: suma a los multiplicadores
    public override void Apply(PlayerPowerUpController player)
    {
        player.AddBulletSpeed(addBulletSpeedMultiplicador);
        player.AddSpawnMultiplicador(addSpawnmultiplicador);
        player.AddBulletDamageMultiplicador(addDamageMultiplicador);

        // Si tu sistema usa duración con HandleTemporalPowerUp:
        if (duracion > 0)
        {
            player.StartCoroutine(player.HandleTemporalPowerUp(this, duracion));
        }
        GameManagerUI.Instance.ShowPowerUpMessage("SHOTBOOST ACTIVO");
        Debug.Log("ShotBoost aplicado: speed+" + addBulletSpeedMultiplicador + " fire+" + addSpawnmultiplicador + " dmg+" + addDamageMultiplicador);
    }

    public override void Remove(PlayerPowerUpController player)
    {
        player.RemoveBulletSpeed(addBulletSpeedMultiplicador);
        player.RemoveSpawnMultiplicador(addSpawnmultiplicador);
        player.RemoveBulletDamageMultiplicador(addDamageMultiplicador);

        Debug.Log("ShotBoost removido");
    }
}
