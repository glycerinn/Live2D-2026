using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [Header("Timer")]
    public float startTime = 300f; // 5 minutes

    [Header("UI")]
    public TMP_Text timerText;

    private float currentTime;
    private bool timerRunning = true;

    void Start()
    {
        currentTime = startTime;
        UpdateTimerText();
    }

    void Update()
    {
        if (!timerRunning)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            timerRunning = false;

            // Time's up!
            Debug.Log("Game Over");
        }

        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = $"Shift ends in: {minutes:00}:{seconds:00}";
    }

    public float GetRemainingTime()
    {
        return currentTime;
    }
}