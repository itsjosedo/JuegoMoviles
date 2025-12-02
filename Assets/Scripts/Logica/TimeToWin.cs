using UnityEngine;

public class TimeToWin : MonoBehaviour
{
    [SerializeField] public float timeToWin;
    [SerializeField] private GameObject canvasWin;
    private float time;



    private void Start()
    {
        time = timeToWin;
    }
    void Update()
    {
        time -= Time.deltaTime;

        if(time < 0)
        {
            canvasWin.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
