using UnityEngine;

public class Gun : MonoBehaviour
{
    public ItemData gunItemData;

    public Transform firePoint;     // откуда стреляем

    Camera playerCamera;
    float nextFireTime = 0f;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
    }

    void Shoot()
    {
        //// Устанавливаем время следующего выстрела
        //nextFireTime = Time.time + ItemData.;

        //// Бросаем луч из центра камеры
        //Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //RaycastHit hit;

        //if (Physics.Raycast(ray, out hit, range))
        //{
        //    // Попали - ищем Target
        //    Enemy target = hit.collider.GetComponent<Enemy>();
        //    if (target != null)
        //    {
        //        target.TakeDamage(damage);
        //        Debug.Log($"Попадание! Урон: {damage}");
        //    }
        //}
    }
}
