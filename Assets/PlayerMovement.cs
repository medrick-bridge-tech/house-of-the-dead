using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float rotationSpeed = 7f;
    [SerializeField] private Joystick joystick;
    private Rigidbody rb;

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
        float horizontal = joystick.Vertical;
        float vertical = joystick.Horizontal;

        Vector3 direction = new Vector3(-horizontal, 0f, vertical).normalized;

        if (rb != null)
        {
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}