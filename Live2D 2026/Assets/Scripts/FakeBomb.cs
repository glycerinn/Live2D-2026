using UnityEngine;
using System.Collections;

public class FakeBomb : MonoBehaviour, IBombInteraction
{
    public int fakeBombDamage = 99;

    [Header("Sprites")]
    public Sprite activatedSprite;

    private SpriteRenderer spriteRenderer;
    private bool activated;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Interact(Bomb bomb)
    {
        if (activated)
            return;

        activated = true;

        // Change sprite once
        if (activatedSprite != null)
            spriteRenderer.sprite = activatedSprite;

        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        Debug.Log("3");
        yield return new WaitForSeconds(1);

        Debug.Log("2");
        yield return new WaitForSeconds(1);

        Debug.Log("1");
        yield return new WaitForSeconds(1);

        PlayerHealth.Instance.TakeDamage(fakeBombDamage);

        Destroy(gameObject);
    }
}