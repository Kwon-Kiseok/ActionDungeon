using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainTitleUI : MonoBehaviour
{
    [SerializeField] private Button gameStartButton;
    [SerializeField] private Button exitButton;

    private void Start() {
        gameStartButton.onClick.AddListener(() => {
            GameStart();
        });

        exitButton.onClick.AddListener(() => {
            GameExit();
        });
    }

    private void GameStart()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    private void GameExit()
    {
        Application.Quit();
    }
}
