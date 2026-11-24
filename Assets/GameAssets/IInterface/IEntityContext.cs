using UnityEngine;

public interface IEntityContext
{
    CharacterController _characterController { get; }
    Camera _unitCamera {  get; }
    Transform _unitBody { get; }

    
    UnitFacts _unitFacts {  get; }
    IUnitStateReader _unitStateReader { get; }
    IUnitStateWriter _unitStateWriter { get; }
    IMotorProvider MotorProvider { get; }
    //IStaminable StaminaSystem { get; }

    IInputProvider InputProvider { get; }


    IMovementConfig MovementConfig { get; }
    ISpeedStatsProvider SpeedStatsProvider { get; }
    IHeightStatsProvider HeightStatsProvider { get; }
    IJumpStatsProvider JumpStatsProvider { get; }
    

    IStamineStatsProvider StamineStatsProvider { get; }
    IHealthStatsProvider HealthStatsProvider { get; }
    

    IInventoryProvider InventoryData { get; }
    IEquipmentDataProvider EquipmentDataProvider { get; }
}
