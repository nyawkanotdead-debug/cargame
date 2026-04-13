using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private Button _playButton;
    [SerializeField] private string _mainSceneName = "GameScene"; // Название твоей основной сцены

    void Start()
    {
        // Подписываемся на событие окончания видео
        _videoPlayer.loopPointReached += OnVideoFinished;
        _playButton.onClick.AddListener(StartGame);
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Когда видео закончилось, показываем кнопку
        _playButton.gameObject.SetActive(true);
        Cursor.visible = true; // Показываем курсор, чтобы нажать кнопку
        Cursor.lockState = CursorLockMode.None;
    }

    void StartGame()
    {
        // Загружаем основную игру
        SceneManager.LoadScene(_mainSceneName);
    }
}