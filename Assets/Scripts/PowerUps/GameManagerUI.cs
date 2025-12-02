using UnityEngine;

public class GameManagerUI : MonoBehaviour
{
    public static GameManagerUI Instance;

    public PowerUpUI powerUpMessageUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowPowerUpMessage(string message)
    {
        powerUpMessageUI.ShowMessage(message);
    }
}
