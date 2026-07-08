using UnityEngine;

public interface IHeightStatsProvider
{
    float GetCurrentHeight();

    float GetStandHeight();
    float GetCrouchHeight();
    float GetProneHeight();
}
