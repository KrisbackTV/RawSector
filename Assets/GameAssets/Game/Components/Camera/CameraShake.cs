using UnityEngine;
using VContainer;

public class CameraShake : MonoBehaviour
{
    private AssetsLibrarySO _assetsLibrarySO;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO)
    {
        _assetsLibrarySO = assetsLibrarySO;

        if (_assetsLibrarySO == null) Debug.Log("library == null");
    }


    private CharacterController _enemyCharacter;
    private Vector3 _startLocalPos;

    [SerializeField] private float _bobSpeed = 14f;          // Базовая частота шага
    [SerializeField] private float _bobXAmount = 0.04f;       // Амплитуда влево-вправо
    [SerializeField] private float _bobYAmount = 0.06f;       // Амплитуда вверх-вниз
    [SerializeField] private float _resetSmoothSpeed = 10f;   // Скорость возврата в центр

    private float _timer = 0f;

    private void Awake()
    {
        _enemyCharacter = GetComponentInParent<CharacterController>();
        _startLocalPos = transform.localPosition;

        if( _enemyCharacter == null ) Debug.Log("CharacterController == null");
    }

    private void Update()
    {
        if (_enemyCharacter == null) return;

        Vector3 horizontalVelocity = new Vector3(_enemyCharacter.velocity.x, 0, _enemyCharacter.velocity.z);
        float speed = horizontalVelocity.magnitude;

        if (_enemyCharacter.isGrounded && speed > 0.1f)
        {
            _timer += Time.deltaTime * _bobSpeed * (speed / 5f);

            float waveX = Mathf.Cos(_timer) * _bobXAmount;
            float waveY = Mathf.Sin(_timer * 2f) * _bobYAmount;

            transform.localPosition = new Vector3(
                _startLocalPos.x + waveX,
                _startLocalPos.y + waveY,
                _startLocalPos.z
                );
        }
        else
        {
            _startLocalPos.y = transform.localPosition.y;
            transform.localPosition = Vector3.Lerp(transform.localPosition, _startLocalPos, Time.deltaTime * _resetSmoothSpeed);
        }
    }
}
