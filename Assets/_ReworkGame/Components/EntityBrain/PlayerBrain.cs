using UnityEngine;

public class PlayerBrain : MonoBehaviour, IEntityContext
{
    private InputService _inputService;
    private PlayerData _playerData;


    public CharacterController _characterController { get; set; }
    public Camera _unitCamera {  get; set; }
    public Transform _unitBody {  get; set; }
    public UnitFacts _unitFacts { get; set; }
    public IUnitStateReader _unitStateReader { get; set; }
    public IUnitStateWriter _unitStateWriter { get; set; }
    public IMotorProvider MotorProvider { get; private set; }
    public IInputProvider InputProvider => _inputService;
    public IMovementConfig MovementConfig => _playerData._playerRuntimeData;
    public ISpeedStatsProvider SpeedStatsProvider => _playerData._playerRuntimeData;
    public IHeightStatsProvider HeightStatsProvider => _playerData._playerRuntimeData;
    public IJumpStatsProvider JumpStatsProvider => _playerData._playerRuntimeData;
    public IStamineStatsProvider StamineStatsProvider => _playerData._playerRuntimeData;
    public IHealthStatsProvider HealthStatsProvider => _playerData._playerRuntimeData;
    public IInventoryProvider InventoryData => _playerData._playerInventory;
    public IEquipmentDataProvider EquipmentDataProvider => _playerData._equipmentData;



    public void Initialize(InputService inputService, PlayerData playerData)
    {
        if (inputService == null || playerData == null)
        {
            GameLog.LogError("Какой то компонент отсутствует");
        }

        _inputService = inputService;
        _playerData = playerData;


        MotorProvider = GetComponent<IMotorProvider>();
        _characterController = GetComponent<CharacterController>();
        _unitCamera = GetComponentInChildren<Camera>();
        _unitBody = GetComponent<Transform>();


        _unitFacts = new UnitFacts();

        UnitStateStore store = new UnitStateStore();
        _unitStateReader = store;
        _unitStateWriter = store;


        IEntityModule[] modules = GetComponentsInChildren<IEntityModule>();
        foreach (IEntityModule module in modules)
        {
            module.OnEntityInitializer(this);
        }


        Subscribe();

        GameLog.LogInfo("Игрок заспавнился");
    }

    private void Update()
    {
        //GameLog.LogDebug($"CurrentHeight {_playerData._playerRuntimeData.GetHeight(_unitStateReader.UnitEnemyState)}");
    }

    private void Subscribe()
    {
    }

    private void UnSubscriber()
    {
    }

    private void OnDisable() => UnSubscriber();
    private void OnDestroy() => UnSubscriber();


    private void PlayerDead(DamageInfo info)
    {
        if (_unitFacts != null)
        {
            _unitFacts.IsLocked = true;
            GameLog.LogInfo($"Игрок умер, движение заблокировано. Убийца: {info.SourceName}, Последний урон: {info.RawAmount}");
        }
    }
}
