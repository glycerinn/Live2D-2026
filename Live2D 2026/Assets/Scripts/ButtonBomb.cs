using UnityEngine;

public class ButtonBomb : MonoBehaviour, IBombInteraction
{
    Bomb bomb;

    void Awake()
    {
        bomb = GetComponent<Bomb>();
    }

    public void Interact(Bomb bomb)
    {
        // Player clicked it -> punish them
        bomb.Explode(true);
    }

    public void TimerExpired()
    {
        // Timer finished -> harmless explosion
        bomb.Explode(false);
    }
}