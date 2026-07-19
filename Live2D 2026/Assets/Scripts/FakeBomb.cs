using UnityEngine;
using System.Collections;

public class FakeBomb : MonoBehaviour, IBombInteraction
{
    public int fakeBombDamage = 99;

    [Header("Sprites")]
    public Sprite activatedSprite;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool activated;
    private Bomb bomb;

    void Awake()
    {
        bomb = GetComponent<Bomb>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.gravityScale = 0f;
    }

    public void Interact(Bomb bomb)
    {
        if (activated)
            return;

        activated = true;

        bomb.StopCountdown();

        if (activatedSprite != null)
            spriteRenderer.sprite = activatedSprite;

        if (rb != null)
            rb.gravityScale = 1f;

        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(3);

        PlayerHealth.Instance.TakeDamage(fakeBombDamage);

        Destroy(gameObject);
    }

    
}