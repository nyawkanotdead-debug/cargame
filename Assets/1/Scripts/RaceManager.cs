using UnityEngine;
using TMPro; // Если используете TextMeshPro для текста

public class RaceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText; // Ссылка на текст в UI

    private float _currentTime;
    private bool _isRaceStarted = false;
    private bool _isRaceFinished = false;

    void Update()
    {
        if (_isRaceStarted && !_isRaceFinished)
        {
            _currentTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    private void OnTriggerEnter(Collider healthcare)
    {
        // Проверяем, что это именно машина (убедитесь, что на машине висит Tag "Player")
        if (healthcare.CompareTag("Player"))
        {
            if (!_isRaceStarted)
            {
                StartRace();
            }
            else if (!_isRaceFinished)
            {
                FinishRace();
            }
        }
    }

    private void StartRace()
    {
        _isRaceStarted = true;
        _currentTime = 0f;
        Debug.Log("Гонка началась!");
    }

    private void FinishRace()
    {
        _isRaceFinished = true;
        Debug.Log("Финиш! Ваше время: " + _currentTime.ToString("F2"));
    }

    private void UpdateTimerDisplay()
    {
        if (_timerText != null)
        {
            _timerText.text = "Время: " + _currentTime.ToString("F2");
        }
    }
}