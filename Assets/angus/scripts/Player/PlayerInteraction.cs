using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 2.5f;
    public LayerMask interactLayer;

    public Transform characterForwardTransform;
    
    private PlayerInputManager inputManager;

    public void Start()
    {
        inputManager = FindAnyObjectByType<PlayerInputManager>();

        inputManager.tapInteract += TryInteract;
    }

    public void TryInteract()
    {
        Vector3 origin = transform.position;
        Vector3 direction = characterForwardTransform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, interactRange, interactLayer))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (characterForwardTransform == null) return;

        Gizmos.color = Color.green;
        Vector3 origin = transform.position;
        Vector3 dir = characterForwardTransform.forward * interactRange;
        Gizmos.DrawRay(origin, dir);
    }


}
