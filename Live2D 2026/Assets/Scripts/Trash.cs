using UnityEngine;

public class Trash : MonoBehaviour
{
    [Header("Stats")]
    public float lifeTime = 5f;
    public int damage = 1;

    private bool isDragging;
    private Vector3 offset;
    private AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0f)
        {
            Explode();
        }
    }

    void OnMouseDown()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        offset = transform.position - mouse;
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (!isDragging)
            return;

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        transform.position = mouse + offset;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Explode()
    {
        audioManager.playEnemyDieSFX();
        if (PlayerHealth.Instance != null)
        {
            PlayerHealth.Instance.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}