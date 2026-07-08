using System.Collections.Generic;
using UnityEngine;

// Абстрактный класс, чтобы нельзя было создать "просто" событие без типа
public abstract class BaseGameEvent<T> : ScriptableObject
{
    private readonly List<IGameEventListener<T>> listeners = new List<IGameEventListener<T>>();

    public void Raise(T value)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(value);
        }
    }

    public void RegisterListener(IGameEventListener<T> listener) => listeners.Add(listener);
    public void UnregisterListener(IGameEventListener<T> listener) => listeners.Remove(listener);
}

// Интерфейс, который будут "надевать" на себя новые слушатели
public interface IGameEventListener<T>
{
    void OnEventRaised(T value);
}