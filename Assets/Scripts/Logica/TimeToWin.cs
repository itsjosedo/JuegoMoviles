using UnityEngine;

public class TimeToWin : MonoBehaviour
{
    [SerializeField] private float timeToWin;
    [SerializeField] private GameObject canvasWin;
    

    // Update is called once per frame
    void Update()
    {
        timeToWin -= Time.deltaTime;

        if(timeToWin < 0)
        {
            canvasWin.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
