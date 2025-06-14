using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }
    // 由 .inputactions 自動生成的類別
    public GameInput gameInput;

    // 供外部訂閱：移動向量、鏡頭旋轉、蹲/爬、與互動
    public event Action<Vector2> moveInput;
    public event Action<float> RotateCamera;
    public event Action crouchStateInput;
    public event Action crawlStateInput;
    public event Action tapInteract;   // 點一下互動
    public event Action holdInteract;  // 長按互動

    private void Awake()
    {
        // 確保 Singleton 實例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void OnEnable()
    {
        gameInput = new GameInput();

        gameInput.Player.Enable();

        // 2. 訂閱 “Player” Action Map 裡的各個 Action 事件
        //    (a) Move: 讀 Vector2
        gameInput.Player.Move.performed += OnMovePerformed;
        gameInput.Player.Move.canceled += OnMoveCanceled;

        //    (b) Interact: Press(behavior=2) + Hold 都在這裡判別
        gameInput.Player.Interact.performed += OnInteractPerformed;

        gameInput.Player.CameraRotate.performed += OnCameraRotate;
        gameInput.Player.CameraRotate.canceled += OnCameraRotate;


        gameInput.Player.Enable();
    }

    private void OnDisable()
    {
        // 4. 取消訂閱並 Disable，避免記憶體洩漏
        gameInput.Player.Move.performed -= OnMovePerformed;
        gameInput.Player.Move.canceled  -= OnMoveCanceled;
        gameInput.Player.Interact.performed -= OnInteractPerformed;

        gameInput.Player.CameraRotate.performed -= OnCameraRotate;
        gameInput.Player.CameraRotate.canceled -= OnCameraRotate;

        

        gameInput.Player.Disable();
    }

    //─── Move 事件回呼 ───
    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
        moveInput?.Invoke(dir);
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        moveInput?.Invoke(Vector2.zero);
    }

    //─── Interact 事件回呼 ───
    private void OnInteractPerformed(InputAction.CallbackContext ctx)
    {
        // 依 binding 裡設定的 Interactions = "Press(behavior=2),Hold"
        // 這裡判斷是短按（PressInteraction）還是長按（HoldInteraction）
        if (ctx.interaction is HoldInteraction)
        {
            holdInteract?.Invoke();
        }
        else if (ctx.interaction is PressInteraction)
        {
            tapInteract?.Invoke();
        }
    }
    
    public void OnCameraRotate(InputAction.CallbackContext ctx)
    {
        float delta = ctx.ReadValue<float>();
        RotateCamera?.Invoke(delta);
    }

    public void OnCrouch(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
            crouchStateInput?.Invoke();
    }

    public void OnCrawl(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
            crawlStateInput?.Invoke();
    }
}
