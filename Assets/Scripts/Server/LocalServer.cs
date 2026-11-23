
using UnityEngine;

public class LocalServer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CentralMediator mediator;

    [Header("Server Configuration")]
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private Vector3 initialPosition;

    private Vector3 authoritativePosition;

    private void Awake()
    {
        authoritativePosition = initialPosition;
    }

    private void OnEnable()
    {
        mediator.Subscribe<MovementRequest>(ProcessMovement);
    }

    private void OnDisable()
    {
        mediator.Unsubscribe<MovementRequest>(ProcessMovement);
    }

    private void ProcessMovement(MovementRequest request)
    {
        // Limitar la intensidad de la intención.
        Vector2 direction = Vector2.ClampMagnitude(
            request.Direction, 1f
        );

        // El servidor calcula el desplazamiento.
        Vector3 displacement = new Vector3(
            direction.x,
            0f,
            direction.y
        );

        Vector3 nextPosition = authoritativePosition +
            displacement * maxSpeed * Time.fixedDeltaTime;

        // Actualizar el estado autoritativo.
        authoritativePosition = nextPosition;

        // Autorizar el movimiento mediante el Mediador.
        mediator.Publish(
            new MovementApproved(authoritativePosition)
        );
    }
}
