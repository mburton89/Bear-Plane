using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _container;
    [SerializeField] Button _pauseButton;
    [SerializeField] Button _resumeButton;
    [SerializeField] Button _quitButton;

    void Start()
    {
        _pauseButton.onClick.AddListener(HandlePausePressed);
        _resumeButton.onClick.AddListener(HandleResumePressed);
        _quitButton.onClick.AddListener(HandleQuitPressed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_container.activeInHierarchy)
            {
                HandleResumePressed();
            }
            else
            {
                HandlePausePressed();
            }
        }
    }

    void HandlePausePressed()
    {
        _container.SetActive(true);
        Time.timeScale = 0;
    }

    void HandleResumePressed()
    {
        _container.SetActive(false);
        Time.timeScale = 1;
    }

    void HandleQuitPressed()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
