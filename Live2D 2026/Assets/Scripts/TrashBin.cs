using UnityEngine;

public class TrashBin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Bomb bomb = other.GetComponent<Bomb>();

        if (bomb == null)
        {
            return;
        }

        if (!bomb.IsDefused)
        {
            return;
        }

        Destroy(other.gameObject);
    }
}