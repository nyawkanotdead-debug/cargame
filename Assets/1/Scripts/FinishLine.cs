using UnityEngine;
using TMPro; // Если используешь TextMeshPro, иначе просто UnityEngine.UI
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject _resultPanel; // Объект с текстом
    [SerializeField] private TextMeshProUGUI _statusText; // Сам текст
    private bool _raceFinished = false;

    private void OnTriggerEnter(Collider other)
    {
        // Если гонка уже закончена, игнорируем остальных
        if (_raceFinished) return;

        if (other.CompareTag("Player"))
        {
            FinishRace("ВЫ ПОБЕДИЛИ!", Color.green);
        }
        else if (other.CompareTag("NPC"))
        {
            FinishRace("ВЫ ПРОИГРАЛИ!", Color.red);
        }
    }

    private void FinishRace(string message, Color textColor)
    {
        _raceFinished = true;
        _resultPanel.SetActive(true);
        _statusText.text = message;
        _statusText.color = textColor;

        // Останавливаем время в игре
         // Замедление для эффектности

        // Через 3 секунды можно перезагрузить сцену или выйти в меню
        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}