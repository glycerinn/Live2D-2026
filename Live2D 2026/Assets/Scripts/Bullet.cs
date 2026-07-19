using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 1;
    public float lifeTime = 5f;
    public float hitRadius = 0.2f;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Move
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        // Get cursor position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check if the cursor is inside the bullet's hit radius
        if (Vector2.Distance(transform.position, mousePos) <= hitRadius)
        {
            PlayerHealth.Instance.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}