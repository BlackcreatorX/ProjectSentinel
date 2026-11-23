
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private CentralMediator mediator;

    private Rigidbody rb;
    private Vector3 approvedPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        approvedPosition = rb.position;
        
    }

    private void OnEnable()
    {
        mediator.Subscribe<MovementApproved>(ApplyApprovedMovement);
        mediator.Subscribe<MovementApproved>(ApplyApprovedMovement);
        mediator.Subscribe<MovementRejected>(ApplyCorrection);
    }

    private void OnDisable()
    {
     mediator.Unsubscribe<MovementApproved>(ApplyApprovedMovement);
     mediator.Unsubscribe<MovementApproved>(ApplyApprovedMovement);
     mediator.Unsubscribe<MovementRejected>(ApplyCorrection);
    }

    private void ApplyApprovedMovement(MovementApproved message)
    {
        approvedPosition = message.TargetPosition;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(approvedPosition);
    }
    private void ApplyCorrection(MovementRejected message)
{
    approvedPosition = message.CorrectedPosition;

    Debug.LogWarning("[Client] Server correction: " + message.Reason);
}
}
