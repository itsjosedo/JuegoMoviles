using UnityEngine;

public class PowerUpViSpeed : PowerUp
{
    public float spedAmount = 2f;
    public override void Apply(PlayerPowerUpController player)
    {
        player.IncreaseMoveSpeed(spedAmount);
        Debug.Log("Power up aplicado");
    }

    public override void Remove(PlayerPowerUpController player)
    {
        player.ResetMoveSpeed(spedAmount);
        Debug.Log("Power up removido");
    }
}
