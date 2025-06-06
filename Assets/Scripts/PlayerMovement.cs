using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 100f;
    [SerializeField] float jumpDelay = 0.1f;

    InputAction moveAction;
    InputAction jumpAction;
    Vector2 moveInputValue;

    bool canJump = true;

    Rigidbody rb;
    GroundCheck groundCheck;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = GetComponent<GroundCheck>();
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        moveInputValue = moveAction.ReadValue<Vector2>();

        if (jumpAction.IsPressed() && groundCheck.IsGrounded && canJump)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 moveDirection = new(moveInputValue.x, 0, moveInputValue.y);
        transform.Translate(moveSpeed * Time.fixedDeltaTime * moveDirection);
    }

    void Jump()
    {
        rb.AddRelativeForce(Vector3.up * jumpForce);
        StartCoroutine(JumpCooldown());
    }

    IEnumerator JumpCooldown()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpDelay);
        canJump = true;
    }
}
