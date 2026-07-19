using UnityEngine;

public class TrashBin : MonoBehaviour
{
    private AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bomb bomb = other.GetComponent<Bomb>();

        if (bomb != null && bomb.IsDefused)
        {
            audioManager.playTrashSFX();
            Destroy(other.gameObject);
            return;
        }

        Trash trash = other.GetComponent<Trash>();

        if (trash != null)
        {
            audioManager.playTrashSFX();
            GameStats.Instance.AddTrash();
            Destroy(other.gameObject);
        }
    }
}