using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button _playButton;

    void Start()
    {
        _playButton.onClick.AddListener(HandlePlayPressed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandlePlayPressed();
        }
    }

    void HandlePlayPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
