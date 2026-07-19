using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SFX;

    public AudioClip MainMenuBGM;
    public AudioClip GameBGM;
    public AudioClip GameOverBGM;
    public AudioClip clicksfx;
    public AudioClip hurtsfx;
    public AudioClip enemyshootsfx;
    public AudioClip enemydiesfx;

    public static AudioManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void playMainMenuBGM()
    {
        BGM.clip = MainMenuBGM;
        BGM.Play();
    }

    public void playGameBGM()
    {
        BGM.clip = GameBGM;
        BGM.Play();
    }

    public void StopBGM()
    {
        BGM.Stop();
    }

    public void playGameOverBGM()
    {
        BGM.clip = GameOverBGM;
        BGM.Play();
    }

    public void playClickSFX()
    {
        SFX.PlayOneShot(clicksfx);
    }

    public void playHurtSFX()
    {
        SFX.PlayOneShot(hurtsfx);
    }


    public void playEnemyShootSFX()
    {
        SFX.PlayOneShot(enemyshootsfx);
    }

    public void playEnemyDieSFX()
    {
        SFX.PlayOneShot(enemydiesfx);
    }
}