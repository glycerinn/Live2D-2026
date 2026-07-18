using UnityEngine;

public class ShooterBomb : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Bomb bomb;

    public float fireRate = 1f;

    private float timer;

    void Awake()
    {
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().SetDirection(dir);
    }
}