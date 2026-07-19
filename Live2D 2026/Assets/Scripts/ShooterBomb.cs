using UnityEngine;

public class ShooterBomb : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Bomb bomb;

    public float fireRate = 1f;
    private AudioManager audioManager;
    private float timer;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        bomb = GetComponent<Bomb>();
    }

    void Update()
    {
        if (bomb.IsDefused)
            return;

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        Vector2 dir = (mouse - firePoint.position).normalized;

        firePoint.right = dir;

        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            timer = 0;
            Shoot(dir);
        }
    }

    void Shoot(Vector2 dir)
    {
        audioManager.playEnemyShootSFX();
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().SetDirection(dir);
    }
}