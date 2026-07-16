using UnityEngine;
using VContainer;

public class HandsSway : MonoBehaviour
{
    private IInputProvider _inputProvider;

    [Header("Sway Settings")]
    [Tooltip("Сила отставания рук. Чем больше, тем сильнее они улетают в сторону")]
    [SerializeField] private float _swayAmount = 0.05f;

    [Tooltip("Максимальное расстояние, на которое руки могут сместиться")]
    [SerializeField] private float _maxSwayAmount = 0.1f;

    [Tooltip("Скорость возвращения рук в исходную точку")]
    [SerializeField] private float _smoothAmount = 6f;

    private Vector3 _initialPosition;

    [Inject]
    public void Construct(IInputProvider inputProvider)
    {
        _inputProvider = inputProvider;
    }

    private void Start()
    {
        // Запоминаем дефолтное положение рук относительно камеры
        _initialPosition = transform.localPosition;
    }

    private void Update()
    {
        if (_inputProvider == null) return;

        // 1. Получаем ввод взгляда
        Vector2 lookInput = _inputProvider.GetLook();

        // 2. Считаем смещение (инвертируем, чтобы отставало)
        float targetX = -lookInput.x * _swayAmount;
        float targetY = -lookInput.y * _swayAmount;

        // 3. ЖЕСТКИЙ ОГРАНИЧИТЕЛЬ (Clamp)
        // По горизонтали (X) и по вертикали (Y) теперь свои независимые лимиты
        targetX = Mathf.Clamp(targetX, -_maxSwayAmount, _maxSwayAmount);

        // Для вертикали сделаем лимит еще чуть меньше, если нужно, 
        // чтобы руки гарантированно не улетали подбородок или на лоб
        float maxVerticalSway = _maxSwayAmount * 0.4f; // Вертикальный лимит чуть строже горизонтального
        targetY = Mathf.Clamp(targetY, -maxVerticalSway, maxVerticalSway);

        // 4. Формируем финальную позицию
        Vector3 targetPosition = new Vector3(
            _initialPosition.x + targetX,
            _initialPosition.y + targetY,
            _initialPosition.z
        );

        // 5. Плавно интерполируем (Lerp)
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * _smoothAmount);
    }
}