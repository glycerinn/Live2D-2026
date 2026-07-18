using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Stats")]
    public int maxHP = 5;
    public float countdown = 10f;
    public int damage = 1;

    public int CurrentHP { get; private set; }
    public bool IsDefused { get; private set; }

    private IBombInteraction interaction;
    private BombSpawner spawner;

    void Start()
    {
        spawner = FindFirstObjectByType<BombSpawner>();
    }

    private void OnDestroy()
    {
        if (spawner != null)
            spawner.BombRemoved(this);
    }

    void Awake()
    {
        CurrentHP = maxHP;
        interaction = GetComponent<IBombInteraction>();
    }

    void Update()
    {
        if (IsDefused)
            return;

        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            Explode();
        }
    }

    protected virtual void OnTimerExpired()
    {
        Explode(true);
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

        if (CurrentHP <= 0)
        {
            Defuse();
        }
    }

    public void Defuse()
    {
        IsDefused = true;

        Debug.Log(name + " Defused");
    }

    public void Explode(bool damagePlayer = true)
    {
        if (damagePlayer)
        {
            // Damage player
        }

        // Explosion effect
        Destroy(gameObject);
    }
}