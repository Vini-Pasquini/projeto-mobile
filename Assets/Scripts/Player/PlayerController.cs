using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MovementJoystick joystick;

    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (joystick.joystickVec.y != 0)
        {
            rb.linearVelocity = new Vector2(joystick.joystickVec.x, joystick.joystickVec.y) * speed;
        } else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
