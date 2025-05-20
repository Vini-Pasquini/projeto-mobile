using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MovementJoystick joystick;
    public HealthBar healthBar;

    [SerializeField] private int health = 3;
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool isActive = true;
    private Vector2 moveDirection;

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
            {
                rb.linearVelocity = moveDirection * speed;

                myAnimator.SetBool("WalkDown", true);

                if (moveDirection.y < 0)
                {
                    myAnimator.SetBool("WalkUp", false);
                }
                else if (moveDirection.y > 0)
                {
                    myAnimator.SetBool("WalkDown", false);
                    myAnimator.SetBool("WalkUp", true);
                }
            }
            else
            {
                myAnimator.SetBool("WalkDown", false);
                myAnimator.SetBool("WalkUp", false);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            myAnimator.SetBool("WalkDown", false);
            myAnimator.SetBool("WalkUp", false);
        }
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
}
