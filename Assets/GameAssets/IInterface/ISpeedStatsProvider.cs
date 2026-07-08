using UnityEngine;

public interface ISpeedStatsProvider
{
    float GetCurrentSpeed();

    float GetSprintSpeed();
    float GetStandSpeed();
    float GetCrouchSpeed();
    float GetProneSpeed();
}
