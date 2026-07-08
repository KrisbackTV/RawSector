using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerBrain : MonoBehaviour
{
    private AssetsLibrarySO _assetsLibrarySO;
    private IInputProvider _inputProvider;
    private PlayerData _playerData;
    private InventoryService _inventoryService;
    private IObjectResolver _objectResolver;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, IInputProvider inputProvider,PlayerData playerData, InventoryService inventoryService, IObjectResolver objectResolver)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inputProvider = inputProvider;
        _playerData = playerData;
        _inventoryService = inventoryService;
        _objectResolver = objectResolver;

        Subscribe();
    }
    
    private UnitMove _unitMove;
    private CameraMotor _camMotor;
    private CrouchAndProne _crouchAndProne;
    private SprintSystem _sprintSystem;


    private void Awake()
    {
        _unitMove = GetComponent<UnitMove>();
        _camMotor = GetComponent<CameraMotor>();
        _crouchAndProne = GetComponent<CrouchAndProne>();
        _sprintSystem = GetComponent<SprintSystem>();
    }

    //private void Start()
    //{
    //    AddItemToInventory();
    //}

    //[ContextMenu("Refresh Grid Manually")]
    //public void AddItemToInventory()
    //{
    //    ItemInstance item = new ItemInstance(_assetsLibrarySO.cube);
    //    _inventoryService.TryAddItemToFreeSlot(_playerData._playerInventory, item);
    //}

    private void OnEnable()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        if (_inputProvider == null) return;

        if (_unitMove != null) _inputProvider.OnJumpPressed += _unitMove.Jump;

        if (_crouchAndProne != null)
        {
            _inputProvider.OnCrouchPressed += _crouchAndProne.SetCrouchState;
            _inputProvider.OnPronePressed += _crouchAndProne.SetProneState;
        }

        if (_sprintSystem != null)
        {
            _inputProvider.OnSprintPressed += _sprintSystem.SetSprint;
        }

        _inputProvider.OnInventoryPressed += OnPlayerInventory;
    }

    private void Update()
    {
        if (_inputProvider == null) return;

        if (_unitMove != null) _unitMove.SetDirection(_inputProvider.GetMovement());
        if (_camMotor != null) _camMotor.SetDirectionCamera(_inputProvider.GetLook());
        if(_sprintSystem != null) _sprintSystem.SetMovement(_inputProvider.GetMovement());
    }

    private void OnPlayerInventory()
    {
        if (_assetsLibrarySO.OnOpenInventoryEvent != null && _playerData._playerInventory != null)
        {
            _assetsLibrarySO.OnOpenInventoryEvent?.Invoke(_playerData._playerInventory);
        }
    }

    private void UnSubscriber()
    {
        if (_unitMove != null) _inputProvider.OnJumpPressed -= _unitMove.Jump;

        if (_crouchAndProne != null)
        {
            _inputProvider.OnCrouchPressed -= _crouchAndProne.SetCrouchState;
            _inputProvider.OnPronePressed -= _crouchAndProne.SetProneState;
        }

        if (_sprintSystem != null)
        {
            _inputProvider.OnSprintPressed -= _sprintSystem.SetSprint;
        }

        _inputProvider.OnInventoryPressed -= OnPlayerInventory;
    }

    private void OnDisable() => UnSubscriber();
    private void OnDestroy() => UnSubscriber();
}
