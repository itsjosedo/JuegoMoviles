using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public float duracion = 0;
    public abstract void Apply(PlayerPowerUpController player);

    public abstract void Remove(PlayerPowerUpController player);
    private void OnTriggerEnter(Collider collision)
    {
        PlayerPowerUpController player = collision.GetComponent<PlayerPowerUpController>();

        if (player != null)
        {
            Apply(player);

            if(duracion > 0)
            {
                player.StartCoroutine(player.HandleTemporalPowerUp(this, duracion));
            }
            Destroy(gameObject);
        }
    }
}
