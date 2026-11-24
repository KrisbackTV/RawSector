using UnityEngine;
using UnityEngine.EventSystems;



[RequireComponent(typeof(CharacterController))]
public class UnitMotor : MonoBehaviour, IMotorProvider, IEntityModule
{
    private CharacterController _characterController;
    private UnitFacts _unitFacts;


    public Vector3 HorizontalVelocity { get; set; }
    public float VerticalVelocity { get; set; }



    public void OnEntityInitializer(IEntityContext context)
    {
        if (context == null)
        {
            GameLog.LogError("Context отсутствует");
            return;
        }

        _characterController = context._characterController;
        _unitFacts = context._unitFacts;
    }


    private void Update()
    {
        if (_characterController == null || _unitFacts == null) return;


        Vector3 motion = HorizontalVelocity + Vector3.up * VerticalVelocity;
        _characterController.Move(motion * Time.deltaTime);

        _unitFacts._isGrounded = _characterController.isGrounded;

        if (_unitFacts._isGrounded && VerticalVelocity < 0)
        {
            VerticalVelocity = -2f;
        }
    }
}
