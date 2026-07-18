using UnityEngine;

public class ClickToDefuse : MonoBehaviour, IBombInteraction
{
    public int clickDamage = 1;

    public void Interact(Bomb bomb)
    {
        bomb.DamageBomb(clickDamage);
    }
}