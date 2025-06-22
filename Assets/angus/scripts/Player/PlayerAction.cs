using System.Collections;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float crossSpeed = 3f;
    private bool isInteracting = false;

    private CharacterController character;

    void Start()
    {
        character = GetComponent<CharacterController>();
        if (character == null)
        {
            Debug.LogError("CharacterController component not found on PlayerAction object.");
        }
    }
    public void CrossWindow(Vector3 nextPos)
    {
        if (!isInteracting)
        {
            StartCoroutine(CrossWindowCoroutine(nextPos));
            Debug.Log("即將跨越窗戶");
        }
    }
    private IEnumerator CrossWindowCoroutine(Vector3 nextPos)
    {
        float interactingTime = 0.3f;
        isInteracting = true;
        character.enabled = false;

        Debug.Log("開始跨越窗戶（等待中）");

        yield return new WaitForSeconds(interactingTime);

        transform.position = nextPos;

        Debug.Log("完成跨越窗戶");

        isInteracting = false;
        character.enabled = true;   
    }


}
