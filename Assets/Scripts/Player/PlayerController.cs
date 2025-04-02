using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MovementJoystick joystick;

    [SerializeField] private float speed = 5f;
    [SerializeField] private bool isActive = true;
    private Vector2 moveDirection;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            if (joystick.joystickVec.y != 0)
            {
                rb.linearVelocity = new Vector2(joystick.joystickVec.x, joystick.joystickVec.y) * speed;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }

            moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (moveDirection != Vector2.zero)
                rb.linearVelocity = moveDirection * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void SetIsActive(bool active) { this.isActive = active; }
}
