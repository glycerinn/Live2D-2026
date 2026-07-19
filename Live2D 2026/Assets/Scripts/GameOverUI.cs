using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;

    public GameObject panel;
    public Image resultImage;

    public Sprite winSprite;
    public Sprite loseSprite;

    private void Awake()
    {
        Instance = this;
        Debug.Log("GameOverUI Awake");
    }

    public void ShowResult(bool win)
    {
        Debug.Log("ShowResult called");
        panel.SetActive(true);
        resultImage.sprite = win ? winSprite : loseSprite;

        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Main Menu");
    }
}