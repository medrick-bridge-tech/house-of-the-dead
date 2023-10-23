using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 3.0f;
    [SerializeField] private float rotationSpeed = 7f;
    [SerializeField] private Joystick joystick;
    private Rigidbody rb;
    private float joystickRange = 0.5f;
    private float joystickMagnitude;
    private float currentSpeed;
    private float horizontal, vertical;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        horizontal = joystick.Vertical;
        vertical = joystick.Horizontal;

        Vector3 direction = new Vector3(-horizontal, 0f, vertical).normalized;

        if (rb != null)
        {
            CalculateSpeed();

            rb.MovePosition(rb.position + direction * currentSpeed * Time.deltaTime);

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void CalculateSpeed()
    {
        joystickMagnitude = Mathf.Max(Mathf.Abs(vertical), Mathf.Abs(horizontal));
        currentSpeed = joystickMagnitude > 0.01f ? (joystickMagnitude < joystickRange ? walkSpeed : runSpeed) : 0;
    }

    public string ReturnState()
    {
        if (currentSpeed > 0)
            return joystickMagnitude < joystickRange ? AnimationNames.WALK : AnimationNames.RUN;
        
        return AnimationNames.IDLE;
    }
}