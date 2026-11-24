using System;
using UnityEngine;

public class WorldTimeTracker : ITimeProvider
{
    private float _totalSeconds;

    // Свойство из интерфейса ITimeProvider, которое будет читать наш логгер
    public string CurrentGameTime
    {
        get
        {
            // Переводим накопленные секунды в красивый формат ЧЧ:ММ:СС
            TimeSpan timeSpan = TimeSpan.FromSeconds(_totalSeconds);
            return timeSpan.ToString(@"hh\:mm\:ss");
        }
    }

    // Этот метод вызывается автоматически каждый кадр благодаря интерфейсу ITickable от VContainer
    public void Tick()
    {
        // Накапливаем время. 
        // Если в будущем захочешь ускорить игровое время (например, день сменяет ночь быстрее), 
        // можно будет просто умножать Time.deltaTime на какой-нибудь множитель.
        _totalSeconds += Time.deltaTime;
    }
}