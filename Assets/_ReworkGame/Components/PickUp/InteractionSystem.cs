using UnityEngine;

public class InteractionSystem : MonoBehaviour, IEntityModule
{
    private UnitFacts _unitFacts;
    private IInputProvider _inputProvider;

    private Transform _pCamera;
    private IInteractable _previewInteract;

    public float _picUpDistance = 2f;


    public void OnEntityInitializer(IEntityContext context)
    {
        if (context == null) return;

        _unitFacts = context._unitFacts;
        _inputProvider = context.InputProvider;
        _pCamera = context._unitCamera.transform;

        Subscribe();
    }


    private void Update()
    {
        IInteractable interactable = ScanForInteractable();

        if (interactable != null)
        {
            if (interactable != _previewInteract)
            {
                _previewInteract = interactable;
                _unitFacts.CurrentInteractable = _previewInteract;
                GameLog.LogInfo($"Найден предмет: {interactable.GetInteractText()}");
            }
        }
        else
        {
            if (_previewInteract != null)
            {
                _previewInteract = null;
                _unitFacts.CurrentInteractable = _previewInteract;
            }
        }


    }



    private void Interactable()
    {
        if (_unitFacts.CurrentInteractable != null)
        {
            _unitFacts.CurrentInteractable.Interact(this.gameObject);
        }
    }

    private IInteractable ScanForInteractable()
    {
        Ray ray = new Ray(_pCamera.position, _pCamera.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _picUpDistance))
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
        if (_pCamera == null) return;

        Ray ray = new Ray(_pCamera.position, _pCamera.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * _picUpDistance);
    }


    private void Subscribe()
    {
        _inputProvider.OnInteractPressed += Interactable;
    }

    private void Unsubscribe()
    {
        _inputProvider.OnInteractPressed -= Interactable;
    }

    private void OnDisable() => Unsubscribe();
}
