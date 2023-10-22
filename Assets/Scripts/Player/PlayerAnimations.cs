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

        bool isWalking = state == "Walk";
        bool isRunning = state == "Run";

        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsRunning", isRunning);

        if (Input.GetKeyDown(KeyCode.T))
        {
            animator.SetTrigger("Throw");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetTrigger("PickUp");
        }
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            animator.SetTrigger("AttemptOpenDoor");
        }
    }
}