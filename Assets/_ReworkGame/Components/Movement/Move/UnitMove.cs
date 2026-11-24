using UnityEngine;



public class UnitMove : MonoBehaviour, IEntityModule
{
    private UnitFacts _unitFacts;
    private IInputProvider _inputProvider;

    private IUnitStateReader _unitStateReader;
    private IMovementConfig _movementConfig;
    
    private IMotorProvider _motorProvider;


    private bool _isLocked = false;
    private Vector3 _moveDirection;



    public void OnEntityInitializer(IEntityContext enemyContext)
    {
        if (enemyContext == null)
        {
            GameLog.LogError("Какой то из обязательных компонентов отсутствует");
        }
        _unitFacts = enemyContext._unitFacts;

        _inputProvider = enemyContext.InputProvider;

        _unitStateReader = enemyContext._unitStateReader;

        _movementConfig = enemyContext.MovementConfig;

        _motorProvider = enemyContext.MotorProvider;

        Subscribe();
    }


    private void Update()
    {
        if (_movementConfig == null || _motorProvider == null) return;

        _moveDirection = _inputProvider.GetMovement;

        if (_unitFacts.IsLocked)
            _moveDirection = Vector3.zero;


        Vector3 worldMove = transform.TransformDirection(_moveDirection);
        _motorProvider.HorizontalVelocity = worldMove * _movementConfig.GetSpeed(_unitStateReader.UnitEnemyState, _unitStateReader.UnitMovementState);

        if (_unitFacts == null) return;

        _unitFacts.MoveInput = new Vector2(worldMove.x, worldMove.y);
    }
    
    public void SetLocked(bool value) => _isLocked = value;

    private void Subscribe() { }
    private void UnSubscribe() { }
    private void OnDestroy() => UnSubscribe();
}
