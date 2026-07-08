using UnityEngine;
using UnityEngine.Events;

public class FloatEventListener : MonoBehaviour, IGameEventListener<float>
{
    public FloatEvent Event; // На какое SO-событие подписываемся
    public UnityEvent<float> Response; // Что делать, когда пришел сигнал

    private void OnEnable()
    {
        if (Event != null) Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (Event != null) Event.UnregisterListener(this);
    }

    public void OnEventRaised(float value) => Response.Invoke(value);
}