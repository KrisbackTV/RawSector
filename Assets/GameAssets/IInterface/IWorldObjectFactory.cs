using UnityEngine;

public interface IWorldObjectFactory
{
    void SpawnItem(ItemInstance item, Vector3 position, Quaternion rotation);
}