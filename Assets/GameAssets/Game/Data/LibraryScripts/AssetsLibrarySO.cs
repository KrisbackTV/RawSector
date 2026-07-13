using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetsLibraryOS", menuName = "Architecture/Data/AssetsLibraryOS")]
public class AssetsLibrarySO : ScriptableObject
{
    [Header("Test Mode")]
    public bool _testSceneForGame = false;

    [Header("Player")]
    public GameObject playerPrefab;
    [Space]
    public PlayerDataSO _playerDataSO;
    [Space]
    public InventorySO _playerInventory;

    [Header("UI")]
    public GameObject UIGridPanelprefab;
    public GameObject UIGridItemSlotPrefab;
    public Sprite backgroundImagePrefab;
    public GameObject UIGhostItemViewPrefab;
    public GameObject _interactMenuPrefab;
    public GameObject _interactButtonPrefab;

    public int cellSize = 100;
    public float _picUpDistance = 1.5f;
    public float _dropDistance = 1.5f;

    [Header("GameState")]
    public List<GameState> _allState;

    [Header("Events")]
    //UIManager
    public Action<WindowType> OnOpenWindowForType;
    public Action OnCloseUIWindow;
    public Action<InventoryData> OnOpenInventoryEvent;

    //InteractSystem
    public Action<string> OnInteractItemName;

    //Esc pressed
    public Action OnEscapePressed;

    //StateMachine
    public Action<GameCoreState> OnChangeStateEvent;
    public Action<GameCoreState> OnPushStateEvent;
    public Action OnPopStateEvent;

    //временно
    public float _sensivity = 10f;

    public GameState GetGameStateForType(GameCoreState state)
    {
        return _allState.FirstOrDefault(s => s._state == state);
    }
}
