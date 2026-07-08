using UnityEngine;

public interface IInteractable
{
    void Interact(GameObject initiator);
    string GetInteractText();
}
