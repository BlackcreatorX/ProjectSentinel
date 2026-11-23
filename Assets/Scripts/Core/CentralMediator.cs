
using System;
using System.Collections.Generic;
using UnityEngine;

public class CentralMediator : MonoBehaviour, IMediator
{
    private readonly Dictionary<Type, Delegate> subscribers = new();

    public void Subscribe<T>(Action<T> handler)
    {
        Type messageType = typeof(T);

        if (subscribers.TryGetValue(messageType, out Delegate existing))
        {
            subscribers[messageType] = Delegate.Combine(existing, handler);
        }
        else
        {
            subscribers.Add(messageType, handler);
        }
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
        Type messageType = typeof(T);

        if (!subscribers.TryGetValue(messageType, out Delegate existing))
            return;

        Delegate updated = Delegate.Remove(existing, handler);

        if (updated == null)
            subscribers.Remove(messageType);
        else
            subscribers[messageType] = updated;
    }

    public void Publish<T>(T message)
    {
        Type messageType = typeof(T);

        if (!subscribers.TryGetValue(messageType, out Delegate existing))
            return;

        foreach (Action<T> handler in existing.GetInvocationList())
        {
            handler.Invoke(message);
        }
    }

    private void OnDestroy()
    {
        subscribers.Clear();
    }
}
