using UnityEngine;

public interface IMotorProvider
{
    Vector3 HorizontalVelocity { get; set; }
    float VerticalVelocity { get; set; }
}