using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    public float speed;
    public Frame testFrame;

    private bool canMove = true;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveInput = Vector2.zero;
    }

    public void OnMoveHor(CallbackContext context)
    {
        if (Mathf.Abs(context.ReadValue<float>()) > 0.1f)
        {
            moveInput = Vector2.zero;
        }
        moveInput.x = context.ReadValue<float>();
    }
    public void OnMoveVer(CallbackContext context)
    {
        if (Mathf.Abs(context.ReadValue<float>()) > 0.1f)
        {
            moveInput = Vector2.zero;
        }
        moveInput.y = context.ReadValue<float>();
    }

    public void OnInteract(CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Interacted");
            FindObjectOfType<DialogueTimeline>().StartFrame(testFrame);
        }
    }

    Vector2 moveInput;

    private void Update()
    {
        if (!canMove)
        {
            return;
        }
        rb.velocity = moveInput * speed;
    }
}
