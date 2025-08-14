using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    public bool isFacingRight { set; get; } = true;
    public bool onPlatform;
    
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerSFX sfx;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    private float glidingSpeed = 1f;
    private float initialGravityScale;

    private float fallSpeedYDampingChangeThreshold;

    private bool isMoving;

    private CameraFollowObject cameraFollowObject;

    private Transform interestMark;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject cameraFollowOB;
    [SerializeField] private PhysicsMaterial2D withFriction;
    [SerializeField] private PhysicsMaterial2D withoutFriction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sfx = GetComponent<PlayerSFX>();

        initialGravityScale = rb.gravityScale;

        cameraFollowObject = cameraFollowOB.GetComponent<CameraFollowObject>();

        interestMark = GameObject.FindGameObjectWithTag("Interest").GetComponent<Transform>();

        NotInterested();

        fallSpeedYDampingChangeThreshold = CameraManager.instance.fallSpeedYDampingChangeThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHorizontalMovement();
        Jump();
        Glide();
        PlayerAnimations();

        if (rb.velocity.y < fallSpeedYDampingChangeThreshold && !CameraManager.instance.IsLerpingYDamping && !CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }

        if (rb.velocity.y >= 0f && !CameraManager.instance.IsLerpingYDamping && CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpedFromPlayerFalling = false;

            CameraManager.instance.LerpYDamping(false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        TurnCheck();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Elevator"))
        {
            transform.parent = collision.gameObject.transform;
            onPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Elevator"))
        {
            transform.parent = null;
            onPlatform = false;
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(0.4375525f, 1.068364f), 0f, groundLayer);
    }

    public void PlayerHorizontalMovement()
    {
        //Horizontal Movement

        horizontal = Input.GetAxis("Horizontal");

        if (horizontal == 0f)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }

    public void CheckSlopes()
    {
        if (isMoving == true)
        {
            rb.sharedMaterial = withoutFriction;
        }
        else
        {
            rb.sharedMaterial = withFriction;
        }
    }

    public void Jump()
    {
        //Coyote Time

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        //Jump Buffer

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        //Jump Handling

        ///If the jump button is held
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            animator.SetTrigger("Jump");

            jumpBufferCounter = 0f;
        }

        ///If the jump button is released
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        

    }

    public void Glide()
    {
        //Gliding

        if (Input.GetMouseButton(0) && rb.velocity.y <= 0f && !IsGrounded())
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, -glidingSpeed);
            animator.SetBool("Gliding", true);
        }
        else
        {
            rb.gravityScale = initialGravityScale;
            animator.SetBool("Gliding", false);
        }
    }

    private void TurnCheck()
    {
        if (horizontal > 0 && !isFacingRight)
        {
            Turn();
        }
        else if (horizontal < 0 && isFacingRight)
        {
            Turn();
        }
    }

    private void Turn()
    {
        if (isFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
            cameraFollowObject.CallTurn();
        }
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
            cameraFollowObject.CallTurn();
        }
    }

    private void PlayerAnimations()
    {
        animator.SetBool("Running", isMoving);

        if (isMoving == true)
        {
            sfx.PlayFootsteps();
        }

        if (isMoving == false)
        {
            animator.SetTrigger("Stop");
        }

        if (!IsGrounded())
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        if (Input.GetMouseButtonDown(0) && !IsGrounded())
        {
            animator.SetTrigger("GlideStart");
            animator.SetBool("GlidePressed", true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("GlidePressed", false);
        }
    }

    public void Interested()
    {
        interestMark.gameObject.SetActive(true);
    }

    public void NotInterested()
    {
        interestMark.gameObject.SetActive(false);
    }
}
