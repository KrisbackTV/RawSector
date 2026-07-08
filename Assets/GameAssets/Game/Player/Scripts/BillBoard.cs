using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Это ссылка на камеру
    private Transform cameraTransform;

    void Start()
    {
        // Находим камеру при старте
        cameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        // Заставляем объект смотреть на камеру
        // Но! Мы не хотим, чтобы он наклонялся вверх-вниз, поэтому смотрим только по горизонтали

        // Направление от объекта к камере
        Vector3 directionToCamera = cameraTransform.position - transform.position;

        // Убираем наклон по вертикали (Y), чтобы полоска не заваливалась
        directionToCamera.y = 0;

        // Поворачиваем объект в этом направлении
        if (directionToCamera != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(-directionToCamera);
            // Используем -directionToCamera, чтобы передняя сторона смотрела на камеру
        }
    }
}