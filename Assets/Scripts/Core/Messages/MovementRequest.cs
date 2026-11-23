
using UnityEngine;

public class MovementRequest
{
    public Vector2 Direction { get; }

    public MovementRequest(Vector2 direction)
    {
        Direction = direction;
    }
}
