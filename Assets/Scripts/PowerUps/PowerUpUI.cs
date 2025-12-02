using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PowerUpUI : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public float displayTime = 1.5f;
    public float fadeSpeed = 2f;

    private Coroutine messageCoroutine;

    private void Start()
    {
        messageText.alpha = 0; // Invisible al iniciar
    }

    public void ShowMessage(string msg)
    {
        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        messageCoroutine = StartCoroutine(ShowMessageRoutine(msg));
    }

    IEnumerator ShowMessageRoutine(string msg)
    {
        messageText.text = msg;

        // FADE IN
        while (messageText.alpha < 1)
        {
            messageText.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        // Esperar
        yield return new WaitForSeconds(displayTime);

        // FADE OUT
        while (messageText.alpha > 0)
        {
            messageText.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }
}
