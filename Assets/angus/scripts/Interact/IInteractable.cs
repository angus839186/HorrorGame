using UnityEngine;

public interface IInteractable
{
    string GetInteractObjectName();
    void Interact(GameObject interactor);
}
