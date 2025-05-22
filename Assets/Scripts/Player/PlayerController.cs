using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MovementJoystick joystick;
    public HealthBar healthBar;

    [SerializeField] private int health = 3;
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool isActive = true;
    private Vector2 moveDirection;

    private float speedMultiplier = 1f;

    private Rigidbody2D rb;
    private Animator myAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
        healthBar.SetHealth(health);
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            myAnimator.SetBool("WalkDown", false);
            myAnimator.SetBool("WalkUp", false);
            myAnimator.SetBool("WalkRight", false);
            myAnimator.SetBool("WalkLeft", false);

            if (joystick.joystickVec.y != 0)
            {
                rb.linearVelocity = new Vector2(joystick.joystickVec.x, joystick.joystickVec.y) * speed * speedMultiplier;

                if (Mathf.Abs(joystick.joystickVec.x) > Mathf.Abs(joystick.joystickVec.y))
                {
                    if (joystick.joystickVec.x < 0)
                    {
                        myAnimator.SetBool("WalkLeft", true);
                    }
                    else if (joystick.joystickVec.x > 0)
                    {
                        myAnimator.SetBool("WalkRight", true);
                    }
                }
                else
                {
                    if (joystick.joystickVec.y < 0)
                    {
                        myAnimator.SetBool("WalkDown", true);
                    }
                    else if (joystick.joystickVec.y > 0)
                    {
                        myAnimator.SetBool("WalkUp", true);
                    }
                }
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }

            moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (moveDirection != Vector2.zero)
            {
                rb.linearVelocity = moveDirection * speed;


                if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y))
                {
                    if (moveDirection.x < 0)
                    {
                        myAnimator.SetBool("WalkLeft", true);
                    }
                    else if (moveDirection.x > 0)
                    {
                        myAnimator.SetBool("WalkRight", true);
                    }
                }
                else
                {
                    if (moveDirection.y < 0)
                    {
                        myAnimator.SetBool("WalkDown", true);
                    }
                    else if (moveDirection.y > 0)
                    {
                        myAnimator.SetBool("WalkUp", true);
                    }
                }
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            myAnimator.SetBool("WalkDown", false);
            myAnimator.SetBool("WalkUp", false);
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void TakeHit()
    {
        Debug.Log("Tomou dano");
        health--;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            GameManager.Instance.PlayerDeath();
        }
    }

    public void SetIsActive(bool active) { this.isActive = active; }

    public void SetSpeedMultiplier(float multiplier)
    {
        this.speedMultiplier = multiplier;
    }
}
