using System.Collections;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scene")]
    public int nextSceneIndex = 1;
    private bool isLoading = false;
    public GameObject CreditsPanel;

    // private AudioManager audioManager;

    // public void Awake()
    // {
    //     audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    // }

    // void Start()
    // {
    //     audioManager.playMainMenuBGM();
    // }

    void Update()
    {
        if (CreditsPanel.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            QuitGame();
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ShowCredits();
            return;
        }

        if (Input.anyKeyDown)
        {
            if (isLoading)
            return;

            isLoading = true;
            // audioManager.playLoginSFX();
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    // IEnumerator LoadNextLevel()
    // {
    //     yield return StartCoroutine(Transition.Instance.PlayTransition());

    //     SceneManager.LoadScene(nextSceneIndex);

    //     yield return StartCoroutine(Transition.Instance.EndTransition());
    // }

    void QuitGame()
    {
        Debug.Log("Quitting Game...");

        Application.Quit();
    }

    public void ShowCredits()
    {
        CreditsPanel.SetActive(true);
    }

    public void UnshowCredits()
    {
        CreditsPanel.SetActive(false);
    }
}