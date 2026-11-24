using UnityEngine;

public interface IHeightStatsProvider
{
    float StandHeight { get; }
    float CrouchHeight { get; }
    float ProneHeight { get; }
}
