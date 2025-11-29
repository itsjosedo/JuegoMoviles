using UnityEngine;

public class PowerUpEscudo : PowerUp
{
    public float tiempoEscudo = 0;
    public override void Apply(PlayerPowerUpController player)
    {
        player.ActivarEscudo(tiempoEscudo);
        Debug.Log("Escudo activo");
    }

    public override void Remove(PlayerPowerUpController player)
    {
        player.DesactivarEscudo();
        Debug.Log("Escudo desactivado");
    }
}
