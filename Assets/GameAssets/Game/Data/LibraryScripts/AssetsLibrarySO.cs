using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetsLibraryOS", menuName = "Architecture/Data/AssetsLibraryOS")]
public class AssetsLibrarySO : ScriptableObject
{
    [Header("Test Mode")]
    public bool _testSceneForGame = false;

    public ItemData testItemData;


    [Header("Player")]
    public GameObject playerPrefab;
    [Space]
    public PlayerConfigSo _playerDataSO;
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

    //временно
    public float _sensivity = 10f;
}
