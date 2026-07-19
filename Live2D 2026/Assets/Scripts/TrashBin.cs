using UnityEngine;

public class TrashBin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Bomb bomb = other.GetComponent<Bomb>();

        if (bomb != null && bomb.IsDefused)
        {
            Destroy(other.gameObject);
            return;
        }

        Trash trash = other.GetComponent<Trash>();

        if (trash != null)
        {
            GameStats.Instance.AddTrash();
            Destroy(other.gameObject);
        }
    }
}