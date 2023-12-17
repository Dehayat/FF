using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    public UnityEvent<Character> onTalkToCharacter;

    public float speed;
    public Frame testFrame;
    public DialogueTimeline dialogue;

    private bool canMove = true;
    private Rigidbody2D rb;
    private Animator anim;

    public void CantMove()
    {
        canMove = false;
    }

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

    private Character talkingToCharacter;
    private void InteractWithTrigger()
    {
        if (currentTrigger != null && currentTrigger.CanUse())
        {
            talkingToCharacter = currentTrigger.GetComponentInParent<WorldCharacter>().character;
            var interactingGO = currentTrigger.gameObject;
            dialogue.onFinish.AddListener(() =>
            {
                if (GameController.isLastAct && !talkingToCharacter.finish && talkingToCharacter.currentHeart > 0)
                {
                    talkingToCharacter.finish = true;
                    dialogue.StartFrame(talkingToCharacter.trustDialogue);
                }
                else
                {
                    interactingGO.SetActive(true);
                    dialogue.onFinish.RemoveAllListeners();
                    canMove = true;
                    onTalkToCharacter?.Invoke(talkingToCharacter);
                    talkingToCharacter = null;
                }
            });
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
            rb.velocity = Vector2.zero;
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
        if (collision.TryGetComponent<DoorTrigger>(out var door))
        {
            DoorManager.instance.EnterDoor(door.doorExit);
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
