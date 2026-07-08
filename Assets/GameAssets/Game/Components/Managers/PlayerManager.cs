using UnityEngine;
using VContainer;

public class PlayerManager : MonoBehaviour
{
    private IInputProvider _inputProvider;
    private PlayerStats _playerStats;
    public PlayerData _playerData {  get; private set; }


    [Header("Components")]
    private PlayerBrain _playerBrain;
    private UnitMove _unitMove;
    private CameraMotor _camMotor;
    private CrouchAndProne _crouchMotor;
    private SprintSystem _sprintSystem;
    //[SerializeField] private InteractionSystem interactionSystem;
    //[SerializeField] private HealthSystem healthSystem;
    //[SerializeField] private StamineSystem stamineSystem;
    //[SerializeField] private WeaponSwitcher weaponSwitcher;



    private CharacterController _characterController;
    private GameObject _pGameObject;
    private Transform _pTransform;
    private Camera _pCamera;
    //[SerializeField] private Transform weaponPosition;

    [Inject]
    public void Construct(IInputProvider inputProvider, PlayerStats playerStats, PlayerData playerData)
    {
        _inputProvider = inputProvider;
        _playerStats = playerStats;
        _playerData = playerData;
    }

    private void Awake()
    {
        if (_characterController == null)
            _characterController = GetComponent<CharacterController>();

        if(_pGameObject == null)
            _pGameObject = gameObject;

        if(_pTransform == null)
            _pTransform = transform;

        if(_pCamera == null)
            _pCamera = GetComponentInChildren<Camera>();

        if (_playerBrain == null)
            _playerBrain = GetComponent<PlayerBrain>();

        if(_unitMove == null)
            _unitMove = GetComponent<UnitMove>();

        if( _camMotor == null)
            _camMotor = GetComponentInChildren<CameraMotor>();

        if (_crouchMotor == null)
            _crouchMotor = GetComponent<CrouchAndProne>();

        if(_sprintSystem == null)
            _sprintSystem = GetComponent<SprintSystem>();
    }

    public Transform GetTransform() => transform;
    public IInputProvider GetInput() => _inputProvider;
}
