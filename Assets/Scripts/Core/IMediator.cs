
using System;

public interface IMediator
{
    void Subscribe<T>(Action<T> handler);

    void Unsubscribe<T>(Action<T> handler);

    void Publish<T>(T message);
}
