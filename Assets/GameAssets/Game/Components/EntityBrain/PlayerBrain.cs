using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerBrain : MonoBehaviour, IPlayerInventoryProvider
{
    private AssetsLibrarySO _assetsLibrarySO;
    private IInputProvider _inputProvider;
    private PlayerData _playerData;
    private GameActions _gameActions;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, IInputProvider inputProvider,PlayerData playerData, GameActions gameActions)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inputProvider = inputProvider;
        _playerData = playerData;
        _gameActions = gameActions;
    }
    
    private UnitMove _unitMove;
    private CameraMotor _camMotor;
    private CrouchAndProne _crouchAndProne;
    private SprintSystem _sprintSystem;
    private ItemToHandVisualizer _itemToHandVisualizer;

    public InventoryData PlayerInventory => _playerData?._playerInventory;


    private void Awake()
    {
        _unitMove = GetComponent<UnitMove>();
        if (_unitMove != null) _unitMove.Initialize(_playerData._playerRuntimeData, _playerData._playerRuntimeData, _playerData._playerRuntimeData);

        _camMotor = GetComponent<CameraMotor>();
        if (_camMotor != null) _camMotor.Initialize(_assetsLibrarySO._sensivity);

        _crouchAndProne = GetComponent<CrouchAndProne>();
        if (_crouchAndProne != null) _crouchAndProne.Initialize(_playerData._playerRuntimeData, _playerData._playerRuntimeData);

        _sprintSystem = GetComponent<SprintSystem>();
        if (_sprintSystem != null) _sprintSystem.Initialize(_playerData._playerRuntimeData, _playerData._playerRuntimeData);

        _itemToHandVisualizer = GetComponent<ItemToHandVisualizer>();

        Subscribe();
    }


    private void Update()
    {
        if (_inputProvider == null) return;

        _unitMove.SetDirection(_inputProvider.GetMovement());
        _camMotor.SetDirectionCamera(_inputProvider.GetLook());
        _sprintSystem.SetMovement(_inputProvider.GetMovement());

    }

    private void EquipItem(EquipmentType equipmentType)
    {
        ItemInstance equipItemInstance = null;
        _playerData._equipmentData.GetItemForEquipSlot(equipmentType, (callback) =>
        {
            equipItemInstance = callback;
        });
        
        _itemToHandVisualizer.EquipItemVisual(equipItemInstance);
    }

    private void OnEquipmentSlotUpdated(EquipmentType updatedSlot, ItemInstance newItem)
    {
        if (_itemToHandVisualizer == null) return;

        if (updatedSlot == _playerData._playerRuntimeData.ActiveSlot)
        {
            _itemToHandVisualizer.EquipItemVisual(newItem);
        }
    }


    private void OnPlayerInventory()
    {
        if (_assetsLibrarySO.OnOpenInventoryEvent != null && _playerData._playerInventory != null)
        {
            _assetsLibrarySO.OnOpenInventoryEvent?.Invoke(_playerData._playerInventory);
        }
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

        if (_itemToHandVisualizer != null)
        {
            _playerData._playerRuntimeData.OnActiveSlotChanged += EquipItem;
            _gameActions.OnUpdateToEquipmentSlot += OnEquipmentSlotUpdated;
        }

        _inputProvider.OnInventoryPressed += OnPlayerInventory;
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

        if (_itemToHandVisualizer != null)
        {
            _playerData._playerRuntimeData.OnActiveSlotChanged -= EquipItem;
            _gameActions.OnUpdateToEquipmentSlot -= OnEquipmentSlotUpdated;
        }

        _inputProvider.OnInventoryPressed -= OnPlayerInventory;
    }

    private void OnDisable() => UnSubscriber();
    private void OnDestroy() => UnSubscriber();
}
