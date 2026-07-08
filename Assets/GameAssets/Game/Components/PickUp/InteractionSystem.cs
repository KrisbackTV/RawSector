using UnityEngine;
using VContainer;

public class InteractionSystem : MonoBehaviour
{
    private AssetsLibrarySO _assetsLibrarySO;
    private InputService _inputService;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, InputService inputService)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inputService = inputService;
    }

    private Transform _pCamera;

    private IInteractable _previewInteract;


    private void Start()
    {
        if (_pCamera == null)
        {
            _pCamera = GetComponentInChildren<Camera>().transform;
        }

        if (_inputService != null)
        {
            _inputService.OnInteractPressed += Interactable;
        }

    }

    private void Update()
    {
        IInteractable interactable = ScanForInteractable();

        if (interactable != null)
        {
            if (interactable != _previewInteract)
            {
                _previewInteract = interactable;
                _assetsLibrarySO.OnInteractItemName?.Invoke(interactable.GetInteractText());
                Debug.Log($"найден предмет: {interactable.GetInteractText()}");
            }
        }
        else
        {
            if (_previewInteract != null)
            {
                _previewInteract = null;
                _assetsLibrarySO.OnInteractItemName?.Invoke(" ");
                Debug.Log($"предмет не найден null");
            }
        }
    }



    private void Interactable()
    {
        IInteractable interactable = ScanForInteractable();

        if (interactable != null)
        {
            interactable.Interact(this.gameObject);
        }
    }

    private IInteractable ScanForInteractable()
    {
        Ray ray = new Ray(_pCamera.position, _pCamera.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _assetsLibrarySO._picUpDistance))
        {
            var interactable = hit.collider.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                return interactable;
            }
            return null;
        }
        return null;
    }

    void OnDrawGizmos()
    {
        if (_assetsLibrarySO == null) return;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * _assetsLibrarySO._picUpDistance);
    }



    private void Unsubscribe()
    {
        if (_inputService != null)
        {
            _inputService.OnInteractPressed -= Interactable;
        }
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
}
