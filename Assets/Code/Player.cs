using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    public float speed;
    public Frame testFrame;
    public DialogueTimeline dialogue;

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
            dialogue.OnInput();
            if (canMove)
            {
                InteractWithTrigger();
            }
        }
    }

    private void InteractWithTrigger()
    {
        if (currentTrigger != null)
        {
            var interactingGO = currentTrigger.gameObject;
            dialogue.onFinish.AddListener(() => { interactingGO.SetActive(true); dialogue.onFinish.RemoveAllListeners(); });
            dialogue.StartFrame(currentTrigger.frame);
            currentTrigger.gameObject.SetActive(false);
            currentTrigger = null;
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

    private FrameTrigger currentTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<FrameTrigger>(out var trigger))
        {
            currentTrigger = trigger;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<FrameTrigger>(out var trigger))
        {
            if (currentTrigger == trigger)
            {
                currentTrigger = null;
            }
        }
    }
}
