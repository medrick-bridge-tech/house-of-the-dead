using Settings;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        string state = playerMovement.ReturnState();

        bool isWalking = state == AnimationNames.WALK;
        bool isRunning = state == AnimationNames.RUN;

        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsRunning", isRunning);

        if (Input.GetKeyDown(Keybindings.ThrowKey))
            animator.SetTrigger("Throw");

        if (Input.GetKeyDown(Keybindings.PickupKey))
            animator.SetTrigger("PickUp");
        
        if (Input.GetKeyDown(Keybindings.AttemptOpenDoorKey))
            animator.SetTrigger("AttemptOpenDoor");
        
    }
    public void ThrowPlateAnimation()
    {
        animator.SetTrigger("Throw");
    }
}