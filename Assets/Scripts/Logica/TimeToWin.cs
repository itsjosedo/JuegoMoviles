using UnityEngine;

public class TimeToWin : MonoBehaviour
{
    [SerializeField] public float timeToWin;
    [HideInInspector] public float time;
    [SerializeField] private GameObject canvasWin;


    private void Start()
    {
        time = timeToWin;
    }
    // Update is called once per frame
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
