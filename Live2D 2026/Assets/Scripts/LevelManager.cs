using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    } 

    void Start()
    {
        audioManager.playGameBGM();
    }
}