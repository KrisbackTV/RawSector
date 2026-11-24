using System;


[Serializable]
public struct StanceStats
{
    public float _standHeight;
    public float _crouchHeight;
    public float _proneHeight;
}

[Serializable]
public struct MovementStats
{
    public float _sprintSpeed;
    public float _standSpeed;
    public float _crouchSpeed;
    public float _proneSpeed;
    public float _jumpForce;
}

[Serializable]
public struct HealthStats
{
    public float _maxHealth;
    public float _healthRegenRate;
    public float _healthRegenDelay;
}

[Serializable]
public struct StaminaStats
{
    public float _maxStamina;
    public float _staminaRegenRate;
    public float _staminaRegenDelay;
    public float _sprintCost;
    public float _jumpCost;
}


public enum WeaponType
{
    Pistol,
    Rifle,
    Shotgun,
    Sniper,
    SMG
}

[Serializable]
public struct GunStats
{
    public WeaponType weaponType;
    public float damage;
    public float roundsPerMinute;
    public float range;
    public float accuracy;
}


public enum AmmoType
{
    Base,
    ArmorPiercing,
    Expansive,
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


[Serializable]
public struct AmmoStats
{
    public AmmoType ammoType;
    public AmmoCaliber ammoCaliber;
    public float damage;
    public float armorPenetration;
    public float bleedChance;
}

public enum DamageType
{
    Kinetic,    // Пули, взрывы
    Bite,       // Мутанты (собаки, кровососы)
    Radiation,  // Радиационные очаги
    Anomaly,    // Электра, Жарка, Карусель
    Bleeding    // Урон от кровотечения
}

public enum HitZone
{
    Default,
    Head,
    Torso,
    Legs
}

public struct DamageInfo
{
    public float RawAmount;
    public DamageType Type;
    public HitZone Zone;
    public string SourceName;

    public DamageInfo(float amount, DamageType type, string sourceName = "Неизвестно", HitZone zone = HitZone.Default)
    {
        RawAmount = amount;
        Type = type;
        SourceName = sourceName;
        Zone = zone;
    }
}

public class GameStruct
{
    
}
