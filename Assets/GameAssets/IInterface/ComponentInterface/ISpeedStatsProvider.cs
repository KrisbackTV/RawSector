using UnityEngine;

public interface ISpeedStatsProvider
{
    float SprintSpeed { get; }
    float StandSpeed { get; }
    float CrouchSpeed {  get; }
    float ProneSpeed { get; }
}
