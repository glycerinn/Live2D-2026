using UnityEngine;

public class HealBomb : MonoBehaviour, IBombInteraction
{
    public int healAmount = 20;

    public void Interact(Bomb bomb)
    {
        // Heal the player
        PlayerHealth.Instance.Heal(healAmount);

        // Remove the bomb
        Destroy(bomb.gameObject);
    }
}