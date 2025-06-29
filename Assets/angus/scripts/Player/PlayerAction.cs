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
    public void CrossWindow()
    {
        if (isInteracting) return;
        isInteracting = true;

        // 關閉 CharacterController 才能用動畫位移
        character.enabled = false;
        animator.SetTrigger("CrossWindow");
    }
    public void SetPosAfterCrossWindow()
    {
        // 先把角色撥到窗戶的另一邊
        // （如果你要動畫以外的位移，或是根據 pointB 再做一次插值，都可以在這裡做）
        character.enabled = true;
        isInteracting = false;
        Debug.Log("完成跨越窗戶");
    }


}
