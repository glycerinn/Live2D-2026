using UnityEngine;

public class SpikeBomb : MonoBehaviour
{
    private Bomb bomb;

    private void Awake()
    {
        bomb = GetComponent<Bomb>();
    }

    private void OnMouseEnter()
    {
        if (bomb.IsDefused)
            return;

        Debug.Log("Spike Hit");

        // PlayerHealth.Instance.TakeDamage(1);
    }
}