using UnityEngine;
using VContainer;

public class CameraMotor : MonoBehaviour
{
    private AssetsLibrarySO _assetsLibrarySO;

    private Transform _pCam;
    private Transform _pBody;

    private bool isLocked = false;

    private float xRotation = 0f;
    private Vector2 direction;

    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO)
    {
        _assetsLibrarySO = assetsLibrarySO;
    }

    private void Awake()
    {
        if (_pCam == null)
            _pCam = GetComponentInChildren<Camera>().transform;

        if (_pBody == null)
            _pBody = GetComponent<Transform>();
    }

    public void SetDirectionCamera(Vector2 dir)
    {
        direction = dir;
    }

    private void Update()
    {
        if (_assetsLibrarySO != null)
        {
            if(isLocked)
                direction = Vector2.zero;

            Rotate(_assetsLibrarySO._sensivity);
        }
    }

    public void Rotate(float sensivity)
    {
        float rotateY = direction.x * sensivity * Time.deltaTime;
        _pBody.transform.Rotate(Vector3.up * rotateY);

        xRotation -= direction.y * sensivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        _pCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void SetLocked(bool value) => isLocked = value;
}
