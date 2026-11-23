
using UnityEngine;

public class MovementRejected
{
    public Vector3 CorrectedPosition { get; }
    public string Reason { get; }

    public MovementRejected(Vector3 position, string reason)
    {
        CorrectedPosition = position;
        Reason = reason;
    }
}
