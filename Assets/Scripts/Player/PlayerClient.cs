
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClient : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CentralMediator mediator;
    [SerializeField] private InputActionReference moveAction;

    private Vector2 movementInput;

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

    private void Update()
    {
        movementInput = moveAction.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        MovementRequest request =
            new MovementRequest(movementInput);

        mediator.Publish(request);
    }
}
