using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float airSpeed = 3f;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 10f;

    Vector2 moveInput;
    int faceDirection;

    Rigidbody2D rb;
    Animator anim;
    TouchingDirections touchingDirections;
    Damageable damageable;

    [SerializeField]
    private bool _isMoving;
    public bool IsMoveing 
    {
        get { return _isMoving; }
        private set 
        { 
            _isMoving = value;
            anim.SetBool(AnimationString.isMoving, _isMoving);
        }
    }

    [SerializeField]
    private bool _isRunning;
    public bool IsRunning 
    {
        get { return _isRunning; }
        private set 
        { 
            _isRunning = value;
            anim.SetBool(AnimationString.isRunning, _isRunning);
        }
    }

    public bool CanMove 
    {
        get { return anim.GetBool(AnimationString.canMove); }
        private set {}
    }

    public float CurrentMoveSpeed
    {
        get 
        {
            if(CanMove)
            {
                if(IsMoveing)
                {
                    if(touchingDirections.IsGround)
                    {
                        if(IsRunning)
                            return runSpeed;
                        else
                            return walkSpeed;
                    }
                    else
                    {
                        //Air
                        return airSpeed;
                    }
                }
                else
                    // idle
                    return 0f;
            }
            else
                //lock move
                return 0f;
        }
    }

    

    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();    
    }

    void Start()
    {
        faceDirection = 1;
    }

    void Update()
    {
        Flip();
    }

    void FixedUpdate() 
    {
        if(!damageable.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        anim.SetFloat(AnimationString.yVelocity, rb.velocity.y);
    }

    void Flip()
    {
        if(moveInput.x != 0 && moveInput.x != faceDirection && CanMove)
        {
            faceDirection *= -1;
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if(CanMove)
        {
            IsMoveing = moveInput != Vector2.zero;
        }
        else
        {
            IsMoveing = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsRunning = true;
        }
        if(context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGround)
        {
            anim.SetTrigger(AnimationString.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            anim.SetTrigger(AnimationString.attack);
        }
    }

    public void OnHit(float amount, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            anim.SetTrigger(AnimationString.rangedAttack);
        }
    }
}
