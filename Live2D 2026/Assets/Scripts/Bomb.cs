using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour
{
    [Header("Stats")]
    public int maxHP = 5;
    public float countdown = 10f;
    public int damage = 1;

    [Header("UI")]
    public Slider healthBar;

    [Header("Sprites")]
    public Sprite defusedSprite;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public int CurrentHP { get; private set; }
    public bool IsDefused { get; private set; }

    private IBombInteraction interaction;
    private BombSpawner spawner;
    private AudioManager audioManager;
    private bool countdownPaused;

    public void StopCountdown()
    {
        countdownPaused = true;
    }

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        CurrentHP = maxHP;
        interaction = GetComponent<IBombInteraction>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Start floating
        if (rb != null)
            rb.gravityScale = 0f;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHP;
            healthBar.value = CurrentHP;
        }
    }

    void Start()
    {
        spawner = FindFirstObjectByType<BombSpawner>();
    }

    private void OnDestroy()
    {
        if (spawner != null)
            spawner.BombRemoved(this);
    }

    void Update()
    {
        if (IsDefused || countdownPaused)
            return;

        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            Explode();
        }
    }

    void OnMouseDown()
    {
        interaction?.Interact(this);
    }

    public void DamageBomb(int amount)
    {
        if (IsDefused)
            return;

        CurrentHP -= amount;

        if (healthBar != null)
            healthBar.value = CurrentHP;

        if (CurrentHP <= 0)
        {
            Defuse();
        }
    }

    public void Defuse()
    {
        IsDefused = true;

        // Change sprite
        if (spriteRenderer != null && defusedSprite != null)
            spriteRenderer.sprite = defusedSprite;

        // Let it fall
        if (rb != null)
            rb.gravityScale = 1f;

        // Hide the health bar
        if (healthBar != null)
            healthBar.gameObject.SetActive(false);

        // Destroy after 3 seconds if not trashed
        StartCoroutine(DestroyAfterDelay());

        Debug.Log(name + " Defused");
    }

    private System.Collections.IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        if (this != null)
            Destroy(gameObject);
    }

    public void Explode(bool damagePlayer = true)
    {
        if (GameOverUI.GameEnded)
            return;
            
        if (damagePlayer)
        {
            PlayerHealth.Instance.TakeDamage(damage);
        }
        audioManager.playEnemyDieSFX();
        Destroy(gameObject);
    }
}