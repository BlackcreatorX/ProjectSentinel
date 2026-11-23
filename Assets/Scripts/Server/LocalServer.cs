
using UnityEngine;

public class LocalServer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CentralMediator mediator;

    [Header("Movement Rules")]
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private Vector2 mapLimits = new Vector2(5f, 5f);
    [SerializeField] private float fixedHeight = 1.5f;

    private Vector3 authoritativePosition;

    private void Awake()
    {
        authoritativePosition = new Vector3(0f, fixedHeight, 0f);
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
        Vector2 direction = request.Direction;

        // Validar intensidad de entrada.
        bool invalidInput = direction.magnitude > 1.01f;

        direction = Vector2.ClampMagnitude(direction, 1f);

        // Calcular destino desde el estado autoritativo.
        Vector3 displacement = new Vector3(
            direction.x,
            0f,
            direction.y
        );

        Vector3 nextPosition = authoritativePosition +
            displacement * maxSpeed * Time.fixedDeltaTime;

        // Corregir límites del escenario.
        nextPosition.x = Mathf.Clamp(
            nextPosition.x, -mapLimits.x, mapLimits.x
        );

        nextPosition.z = Mathf.Clamp(
            nextPosition.z, -mapLimits.y, mapLimits.y
        );

        nextPosition.y = fixedHeight;

        // Detectar anomalías.
        if (invalidInput)
        {
            Debug.LogWarning("[Server] Invalid movement detected.");

            mediator.Publish(new MovementRejected(
                authoritativePosition,
                "Movement intensity exceeded"
            ));

            return;
        }

        authoritativePosition = nextPosition;

        mediator.Publish(new MovementApproved(authoritativePosition));
    }
}
