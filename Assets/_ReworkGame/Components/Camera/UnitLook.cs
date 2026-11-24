using UnityEngine;

public class UnitLook : MonoBehaviour, IEntityModule
{
    private UnitFacts _unitFacts;
    private IInputProvider _inputProvider;

    private float _sensivity = 0f;

    private Transform _pCam;
    private Transform _pBody;

    private float xRotation = 0f;
    private Vector2 direction;

    public void OnEntityInitializer(IEntityContext entityContext)
    {
        if (entityContext == null) return;

        _pCam = entityContext._unitCamera.transform;
        _pBody = entityContext._unitBody;

        _unitFacts = entityContext._unitFacts;

        _inputProvider = entityContext.InputProvider;

        if (entityContext.InputProvider.Sensivity <= 0f)
        {
            GameLog.LogError("Не инициализироованно, sensivity <= 0");
            return;
        }
        
        _sensivity = entityContext.InputProvider.Sensivity;
    }

    private void Update()
    {
        if (_inputProvider == null || _unitFacts == null) return;

        direction = _inputProvider.GetLook;

        if(_unitFacts.IsLocked)
            direction = Vector2.zero;
        
        Rotate(_sensivity);
    }

    public void Rotate(float sensivity)
    {
        float rotateY = direction.x * sensivity;
        _pBody.transform.Rotate(Vector3.up * rotateY);

        xRotation -= direction.y * sensivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        _pCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
