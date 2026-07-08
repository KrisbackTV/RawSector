using UnityEngine;
using UnityEngine.Events;

public class BoolEventListener : MonoBehaviour, IGameEventListener<bool>
{
    public BoolEvent Event; // На какое SO-событие подписываемся
    public UnityEvent<bool> Response; // Что делать, когда пришел сигнал

    private void OnEnable()
    {
        if (Event != null) Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (Event != null) Event.UnregisterListener(this);
    }

    public void OnEventRaised(bool value) => Response.Invoke(value);
}