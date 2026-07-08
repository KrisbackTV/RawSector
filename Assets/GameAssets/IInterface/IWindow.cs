using UnityEngine;

public interface IWindow
{
    void Open();
    void Close();
    GameObject gameObject { get; }
}
