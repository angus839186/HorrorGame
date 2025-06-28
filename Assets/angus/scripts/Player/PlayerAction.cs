using System.Collections;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float crossSpeed = 3f;
    private bool isInteracting = false;

    private CharacterController character;

    private Animator animator;

    void Start()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if (character == null)
        {
            Debug.LogError("CharacterController component not found on PlayerAction object.");
        }
    }
    public void CrossWindow(Vector3 fromPos)
    {
        if (!isInteracting)
        {
            transform.position = fromPos;
            StartCoroutine(CrossWindowCoroutine());
            Debug.Log("即將跨越窗戶");
        }
    }
    private IEnumerator CrossWindowCoroutine()
    {
        isInteracting = true;
        character.enabled = false;
        animator.applyRootMotion = true;
        animator.SetTrigger("CrossWindow");

        yield return null;
    }
    public void SetPosAfterCrossWindow()
    {
        if (character != null)
        character.enabled = true;
        isInteracting = false;
        Debug.Log("完成跨越窗戶");
    }


}
