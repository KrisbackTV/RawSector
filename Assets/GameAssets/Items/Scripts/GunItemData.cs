using UnityEngine;


public enum WeaponType
{
    Pistol,
    Rifle,
    Shotgun,
    Sniper,
    SMG
}


[CreateAssetMenu(fileName = "GunItemData", menuName = "ItemData/GunItemData")]
public class GunItemData : ItemData
{
    public WeaponType WeaponType = WeaponType.Pistol;
    public float damage = 0f;
    public float roundsPerMinute = 100f;
    public float range = 10f;
    public float accuracy = 0.1f;
}
