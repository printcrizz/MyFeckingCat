using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Animator anim;

    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;

    private float horizontal;
    [SerializeField] float speed = 8f;
    public float jumpingPower;
    private bool isFacingRight = true;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    //Wall Jump private variables
    [Header("Wall Jump Variables")]
    [SerializeField] bool canWallJump;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    [SerializeField] float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(8f, 16f);

    //jump variables
    private bool isJumping;
    [SerializeField] int maxJumps;
    private int remainingJumps;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private TrailRenderer tr;

    //dash variables
    [Header("Dash Variables")]
    [SerializeField] bool canDash;
    private bool isDashing;
    [SerializeField] float dashingPower;
    private float dashingTime = 0.1f;
    private float dashingCoolDown = 1f;

    //attack variables
    private bool canAttack =true;
    private float attackTime = 0.25f;
    public LayerMask enemyMask;
    public LayerMask HookCheck;
    public Transform Whip;

    private void Start()
    {
        _distanceJoint.enabled = false;
        anim = GetComponent<Animator>();
        canWallJump = false;
        canAttack = false;
        canDash = false;
        maxJumps = 1;
    }


    void Update()
    {
        if (isDashing)
        {
            return;
        }
        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.P) && canDash)
        {
            StartCoroutine(Dash());
        }

        if(IsGrounded() && !Input.GetButton("Jump"))
        {
            isJumping = false;
            remainingJumps = maxJumps;
            anim.SetBool("isJumping", false);

        }

        if (IsGrounded())//condiciones para tocar el piso
        {
            //anim.SetBool("isJumping", false);
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))//tocar el boton y saltar
        {
            if (IsGrounded() || (isJumping && remainingJumps > 0f))
            {
                isJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                anim.SetBool("isJumping", true);
                remainingJumps--;
            }
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }


        if(jumpBufferCounter>0f && coyoteTimeCounter>0f)//condiciones de salto
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpBufferCounter = 0f;
        }
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.5f );
            coyoteTimeCounter = 0f;
            anim.SetBool("isJumping", true);

        }
        
        WallSlide();
        WallJump();
        Attack();

        if (!isWallJumping)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
            anim.SetFloat("yVelocity", rb.velocity.y);

        }
    }

    private void Flip()
    {
        if(isFacingRight && horizontal <0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            anim.SetBool("isWalling", true);
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        //else if (!IsWalled() && !IsGrounded() && horizontal !=0f)
        //{
        //    isWallSliding = false;
        //    rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y *-1.5f);

        //}
        else
        {
            isWallSliding = false;
            anim.SetBool("isWalling", false);

        }
    }
    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if(Input.GetButtonDown("Jump")&& wallJumpingCounter > 0f && canWallJump)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if(transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localscale = transform.localScale;
                localscale.x *= -1f;
                transform.localScale = localscale;
            }
        }

        Invoke(nameof(StopWallJumping), wallJumpingDuration);
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        anim.SetTrigger("isDashing");
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        //Vector2 var = new Vector2(transform.localScale.x * Mathf.Abs(Input.GetAxis("Horizontal")) , transform.localScale.y * Input.GetAxis("Vertical")).normalized;
        //rb.velocity = new Vector2(var.x *dashingPower, var.y *dashingPower);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        //anim.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.O) && canAttack)
        {
            Debug.Log("Attack");
            StartCoroutine(Attacking());
        }
        else
        {
            return;
        }
    }

    private IEnumerator Attacking()
    {
        canAttack = false;

        //play animation
        anim.Play("Attack");
        if (IsHooked())
        {
            Debug.Log("Hooked");

            //Start HingeJoint2D
        }
        else
        {
            //Attack
        }

        yield return new WaitForSeconds(attackTime);
        canAttack = true;
    }

    private bool IsHooked()
    {
        return Physics2D.OverlapCircle(Whip.position, 0.2f, HookCheck);
        Debug.Log("Hooked");
    }

    public void ActivateWallJump()
    {
        canWallJump = true;
    }

    public void ActivateDoubleJump()
    {
        maxJumps = 2;
    }
    public void ActivateDash()
    {
        canDash = true;
    }

    public void ActivateAttack()
    {
        canAttack = true;
    }
}
