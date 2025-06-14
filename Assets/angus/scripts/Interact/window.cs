using UnityEngine;

public class window : InteractableObject
{
    public override void Interact(GameObject interactor)
    {
        base.Interact(interactor);
        PlayerAction playerAction = interactor.GetComponent<PlayerAction>();
        playerAction.CrossWindow();
    }
}
