using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Architecture/Data/PlayerDataSO")]
public class PlayerDataSO : EnemyDataSO
{
    public InventorySO inventorySO;

    public int equipSlotIndex = 1;

    public int money = 0;

}
