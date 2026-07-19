using UnityEngine;

public class ButtonBomb : MonoBehaviour, IBombInteraction
{
    private Bomb bomb;

    void Awake()
    {
        bomb = GetComponent<Bomb>();
    }

    public void Interact(Bomb bomb)
    {
        // Clicking it hurts the player.
        bomb.Explode(true);
    }
}