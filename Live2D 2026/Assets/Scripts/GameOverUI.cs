using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;
    public static bool GameEnded { get; private set; }

    public GameObject panel;
    public Image resultImage;

    public Sprite winSprite;
    public Sprite loseSprite;
    private AudioManager audioManager;
    private bool isLoading = false;
    public TMP_Text trashCounterText;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        GameEnded = false;
        Instance = this;
        Debug.Log("GameOverUI Awake");
    }
   
    public void ShowResult(bool win)
    {
        if (GameEnded)
        return;

        GameEnded = true;
        Debug.Log("ShowResult called");
        panel.SetActive(true);
        audioManager.playGameOverBGM();
        resultImage.sprite = win ? winSprite : loseSprite;

        trashCounterText.text = $"{GameStats.Instance.trashThrown}";

        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        if (isLoading)
            return;

        isLoading = true;
        audioManager.playClickSFX();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        if (isLoading)
            return;

        isLoading = true;
        audioManager.playClickSFX();

        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return StartCoroutine(Transition.Instance.PlayTransition());

        SceneManager.LoadScene("Main Menu");

        yield return StartCoroutine(Transition.Instance.EndTransition());
    }
}