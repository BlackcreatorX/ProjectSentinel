
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
    }

    private void OnDisable()
    {
        mediator.Unsubscribe<MovementApproved>(ApplyApprovedMovement);
    }

    private void ApplyApprovedMovement(MovementApproved message)
    {
        approvedPosition = message.TargetPosition;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(approvedPosition);
    }
}
