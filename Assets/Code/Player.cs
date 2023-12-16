using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    public float speed;
    public Frame testFrame;
    public DialogueTimeline dialogue;

    private bool canMove = true;
    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        if (currentTrigger != null && currentTrigger.CanUse())
        {
            var interactingGO = currentTrigger.gameObject;
            dialogue.onFinish.AddListener(() => { interactingGO.SetActive(true); dialogue.onFinish.RemoveAllListeners(); canMove = true; });
            dialogue.StartFrame(currentTrigger.frame);
            currentTrigger.gameObject.SetActive(false);
            currentTrigger = null;
            canMove = false;
        }
    }

    Vector2 moveInput;

    private void Update()
    {
        if (!canMove)
        {
            anim.SetBool("Walk", false);
            return;
        }
        rb.velocity = moveInput * speed;
        if (moveInput.magnitude > 0.1f)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    private FrameTrigger currentTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<FrameTrigger>(out var trigger) && trigger.CanUse())
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
