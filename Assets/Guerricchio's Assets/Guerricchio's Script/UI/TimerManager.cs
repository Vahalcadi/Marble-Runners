using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private float elapsedTime;
    private bool isRunning;

    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;

        int seconds = (int)elapsedTime;
        if (seconds != int.Parse(timerText.text))
            timerText.text = seconds.ToString();
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
        timerText.text = "0";
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public int GetElapsedTime()
    {
        return (int)elapsedTime;
    }
}
