using UnityEngine;
using UnityEngine.UI;

public class DeadScreen : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TheGame _game;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnResumeButtonClick()
    {
        gameObject.SetActive(false);
        _game.StartGame();
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}
