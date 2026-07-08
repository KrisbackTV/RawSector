using UnityEngine;


public enum AmmoType
{
    Base,
    ArmorPiercing,
    HollowPoint,
    Incendiary,
    Slug, //только для 12mm
}

public enum AmmoCaliber
{
    Cal_9mm,
    Cal_12mm,
    Cal_545mm,
    Cal_556mm,
    Cal_762mm
}

[CreateAssetMenu(fileName = "AmmoItemData", menuName = "ItemData/AmmoItemData")]
public class AmmoItemData : ItemData
{
    public AmmoType _ammoType = AmmoType.Base;
    public AmmoCaliber _ammoCaliber = AmmoCaliber.Cal_9mm;

    [Header("Combat Stats")]
    public float _damage;
    public float _armorPenetration;
    public float _bleedChance;
}
