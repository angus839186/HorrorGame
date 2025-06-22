using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    public virtual void Interact(GameObject interactor)
    {
        Debug.Log("Interacted with " + this.gameObject.name);
    }
}
