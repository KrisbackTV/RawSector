using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunItemData gunItemData;

    public float damage = 10f;
    public float fireRate = 0.5f;  // секунд между выстрелами
    public float range = 100f;

    public Transform firePoint;     // откуда стреляем

    Camera playerCamera;
    float nextFireTime = 0f;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        // Стрельба по нажатию
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Устанавливаем время следующего выстрела
        nextFireTime = Time.time + fireRate;

        // Бросаем луч из центра камеры
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            // Попали - ищем Target
            Enemy target = hit.collider.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
                Debug.Log($"Попадание! Урон: {damage}");
            }
        }
    }
}
