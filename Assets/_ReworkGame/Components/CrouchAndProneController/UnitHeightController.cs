using UnityEngine;

public class UnitHeightController : MonoBehaviour, IEntityModule
{
    private IUnitStateReader _state;
    private IMovementConfig _config;
    private CharacterController _cc;
    private Camera _camera;

    public float _cameraOffset = 0.2f;

    public void OnEntityInitializer(IEntityContext ctx)
    {
        if (ctx == null) return;

        _state = ctx._unitStateReader;
        _config = ctx.MovementConfig;

        _cc = ctx._characterController;
        _camera = ctx._unitCamera;
    }

    private void Update()
    {
        // Спрашиваем высоту для текущей стойки
        float targetHeight = _config.GetHeight(_state.UnitEnemyState);

        _cc.height = targetHeight;
        if (_camera != null)
        {
            float targetCamPos = (_cc.height / 2f) - _cameraOffset;
            _camera.transform.localPosition = new Vector3(0, targetCamPos, 0);
        }
    }
}