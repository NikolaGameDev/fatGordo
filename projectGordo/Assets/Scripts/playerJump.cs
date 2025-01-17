using UnityEngine;

public class playerJump : MonoBehaviour
{
    public float jumpForce = 10f, coyoteTime = 0.1f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private float coyoteTimeCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGroundStatus();
        HandleJumpInput();
    }

    void CheckGroundStatus()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer))
        {
            Debug.Log("Player is on the ground");
            coyoteTimeCounter = coyoteTime;  // Reset coyote time when grounded
        }
        coyoteTimeCounter -= Time.deltaTime;  // Reduce coyote time counter
    }

    void HandleJumpInput()
    {
        if (coyoteTimeCounter > 0f && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);  // Apply jump force
        coyoteTimeCounter = 0f;  // Disable further jumps during coyote time
    }
}
