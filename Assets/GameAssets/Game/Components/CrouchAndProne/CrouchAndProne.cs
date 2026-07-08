using UnityEngine;
using VContainer;

public class CrouchAndProne : MonoBehaviour
{
    private IEnemyStateStatsProvider _enemyState;
    private IHeightStatsProvider _heightStats;

    private CharacterController _characterController;
    private Camera _enemyCamera;

    [Header("Setting")]
    public float _cameraOffset = 0.2f;

    private bool IsProneToggled = false;
    private bool IsCrouchToggle = false;

    [Inject]
    public void Construct(IEnemyStateStatsProvider enemyState, IHeightStatsProvider heightStats)
    {
        _enemyState = enemyState;
        _heightStats = heightStats;
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _enemyCamera = GetComponentInChildren<Camera>();
    }

    public void SetCrouchState(bool value) => IsCrouchToggle = value;

    public void SetProneState()
    {
        if (IsProneToggled && !CanStandUp()) return;

        IsProneToggled = !IsProneToggled;
    }


    private void Update()
    {
        if (!_characterController.isGrounded && !IsProneToggled && !IsCrouchToggle)
        {
            _enemyState.SetEnemyState(EnemyState.InAir);
        }
        else if (IsProneToggled)
        {
            _enemyState.SetEnemyState(EnemyState.Prone);
        }
        else if (IsCrouchToggle)
        {
            _enemyState.SetEnemyState(EnemyState.Crouch);
        }
        else
        {
            if (_enemyState.GetEnemyState() != EnemyState.Stand)
            {
                if (CanStandUp()) _enemyState.SetEnemyState(EnemyState.Stand);
            }
        }

        float targetHeight = _heightStats.GetCurrentHeight();
        ApplyCrouch(targetHeight);
    }

    private void ApplyCrouch(float value)
    {
        _characterController.height = value;

        if (_enemyCamera != null)
        {
            float targetCamPos = ( _characterController.height / 2f) - _cameraOffset;
            _enemyCamera.transform.localPosition = new Vector3 (0, targetCamPos, 0);
        }
    }


    public bool CanStandUp()
    {
        Vector3 rayStart = transform.position+ _characterController.center + Vector3.up * (_characterController.height / 2f - 0.1f);
        float rayLength = (_heightStats.GetStandHeight() - _characterController.height) + 0.1f;

        Debug.DrawRay(rayStart, Vector3.up *  rayLength, Color.red);

        int layerMask = ~LayerMask.GetMask("Player");

        bool isStandUp = Physics.Raycast(rayStart, Vector3.up, rayLength, layerMask);

        return !isStandUp;
    }


}
