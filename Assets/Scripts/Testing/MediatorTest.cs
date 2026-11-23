
using UnityEngine;

public class MediatorTest : MonoBehaviour
{
    [SerializeField] private CentralMediator mediator;

    private void OnEnable()
    {
        mediator.Subscribe<string>(ReceiveMessage);
    }

    private void OnDisable()
    {
        mediator.Unsubscribe<string>(ReceiveMessage);
    }

    private void Start()
    {
        mediator.Publish("Hello from Project Sentinel!");
    }

    private void ReceiveMessage(string message)
    {
        Debug.Log("[Mediator] Received: " + message);
    }
}
