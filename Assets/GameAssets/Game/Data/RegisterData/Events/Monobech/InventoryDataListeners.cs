using UnityEngine;
using UnityEngine.Events;

public class InventoryDataListeners : MonoBehaviour, IGameEventListener<InventoryData>
{
    public InventoryDataEvent Event; // На какое SO-событие подписываемся
    public UnityEvent<InventoryData> Response; // Что делать, когда пришел сигнал

    private void OnEnable()
    {
        if (Event != null) Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (Event != null) Event.UnregisterListener(this);
    }

    public void OnEventRaised(InventoryData value) //Не забудь и тут поменять на то что подписываешься
    {
        Response.Invoke(value);
    }
}