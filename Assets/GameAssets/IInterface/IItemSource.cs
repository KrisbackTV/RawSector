using System;
using UnityEngine;

public interface IItemSource
{
    //То откуда переносится предмет

    //Метод возврата предмета на место
    public void Sourse(DragContextItem contextItem, Action<bool> callback);

    //Метод который принимает предмет
    public void Receive(DragContextItem contextItem, Action<bool> callback);

    //Мето для удалления предметов
    public void Remove(ItemInstance item, Action<bool> callback);
}
