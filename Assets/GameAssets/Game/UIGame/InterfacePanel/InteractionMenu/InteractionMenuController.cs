using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class InteractionMenuController : MonoBehaviour
{
    private AssetsLibrarySO _assetsLibrarySO;
    private InventoryActions _inventoryActions;
    private IObjectResolver _objectResolver;
    [Inject]
    public void Construct(AssetsLibrarySO assetsLibrarySO, EquipmentService equipmentService, InventoryActions inventoryActions, IObjectResolver objectResolver)
    {
        _assetsLibrarySO = assetsLibrarySO;
        _inventoryActions = inventoryActions;
        _objectResolver = objectResolver;
    }

    [SerializeField] private Transform _canvasTransform;
    [SerializeField] private BackgroundInteractPanel _backgroundInteractPanel;
    private GameObject _interactMenu;

    private GameObject _prefabMenu;
    private GameObject _prefabButton;


    private void Start()
    {
        if (_assetsLibrarySO != null)
        { 
            _prefabMenu = _assetsLibrarySO._interactMenuPrefab;
            _prefabButton = _assetsLibrarySO._interactButtonPrefab;
        }

        if (_backgroundInteractPanel != null)
        {
            _backgroundInteractPanel.Close();
        }

        Subscribe();
    }


    private void OpenInteractMenu(ItemInstance itemInstance, Vector2 mousePos)
    {
        if(_interactMenu != null || itemInstance == null) return;

        _interactMenu = _objectResolver.Instantiate(_prefabMenu, _canvasTransform);
        _interactMenu.transform.localPosition = mousePos;

        ItemActionType actionType = itemInstance._itemData.ItemActionType;

        if (actionType.HasFlag(ItemActionType.Use))
        {
            AddButton("Использовать");
        }
        if (actionType.HasFlag(ItemActionType.Modified))
        {
            AddButton("Осмотреть");
        }
        if (actionType.HasFlag(ItemActionType.Discard))
        {
            AddButton("Выбросить");
        }


        if (_backgroundInteractPanel != null) _backgroundInteractPanel.Open();
    }

    private void AddButton(string name)
    {
        GameObject buttonObj = _objectResolver.Instantiate(_prefabButton, _interactMenu.transform);
        TextMeshProUGUI text = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

        if (text != null)
        {
            text.text = name;
        }

        Button button = buttonObj.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() =>
            {
                CloseInteractMenu();
            });
        }
    }

    private void CloseInteractMenu()
    {
        if (_interactMenu != null)
        {
            Destroy(_interactMenu.gameObject);
        }

        if (_backgroundInteractPanel != null) _backgroundInteractPanel.Close();
    }


    private void Subscribe()
    {
        if (_inventoryActions != null)
        {
            _inventoryActions.OnOpenInteractMenu += OpenInteractMenu;
            _inventoryActions.OnCloseInteractMenu += CloseInteractMenu;
        }
    }


    private void UnSubscribe()
    {
        if (_inventoryActions != null)
        {
            _inventoryActions.OnOpenInteractMenu -= OpenInteractMenu;
            _inventoryActions.OnCloseInteractMenu -= CloseInteractMenu;
        }
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }

    private void OnDisable()
    {
        UnSubscribe();
    }
}
