using UnityEngine;

public class window : InteractableObject
{
    public Transform pointA;
    public Transform pointB;
    public override void Interact(GameObject interactor)
    {
        base.Interact(interactor);
        PlayerAction playerAction = interactor.GetComponent<PlayerAction>();

        Vector3 from = interactor.transform.position;
        Vector3 to = (Vector3.Distance(from, pointA.position) < Vector3.Distance(from, pointB.position))
         ? pointB.position : pointA.position;
        playerAction.CrossWindow(to);
    }
}
