using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private TouchingDirections touchingDirections;
    private Damageable damageable;
    public DetectionZone enmeyZone;

    [SerializeField]
    private float walkAccelerate = 5f;
    [SerializeField]
    private float maxSpeed = 3f;
    [SerializeField]
    private float walkStopRate = 0.05f;

    public enum WalkableDirection { Left, Right }
    private WalkableDirection _walkableDirection;
    private Vector2 walkDirection = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkableDirection; }
        set 
        {
            if(_walkableDirection != value)
            {
                //flip
                gameObject.transform.localScale *= new Vector2(-1, 1);
                if(value == WalkableDirection.Left)
                {
                    walkDirection = Vector2.right;
                }
                else if(value == WalkableDirection.Right)
                {
                    walkDirection = Vector2.left;
                }
            }
            _walkableDirection = value;
        }
    }
    
    public bool CanMove 
    { 
        get { return anim.GetBool(AnimationString.canMove);} 
        private set{ }
    }

    public float AttackCoolDown
    {
        get { return anim.GetFloat(AnimationString.attackCooldown); }
        private set { anim.SetFloat(AnimationString.attackCooldown, Mathf.Max(value, 0)); }
    }

    [SerializeField]
    private bool _hasTarget;
    public bool HasTarget
    { 
        get{ return _hasTarget; } 
        private set
        {
            anim.SetBool(AnimationString.hasTarget, value);
            _hasTarget = value;
        }
    }

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    private void Update() 
    {
        if(AttackCoolDown > 0)
            AttackCoolDown -= Time.deltaTime;
        HasTarget = enmeyZone.detectionCollders.Count > 0;
    }

    private void FixedUpdate()
    {
        if(touchingDirections.IsWall && touchingDirections.IsGround)
        {
            Flip();
        }
        
        if(!damageable.LockVelocity)
        {
            if(CanMove && touchingDirections.IsGround)
                rb.velocity = new Vector2(Mathf.Clamp(walkAccelerate * walkDirection.x * 
                                Time.fixedDeltaTime, -maxSpeed, maxSpeed), rb.velocity.y);
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, walkStopRate), rb.velocity.y);
        }
        
    }

    private void Flip()
    {
        if(WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else if(WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else
        {
            Debug.LogError("WalkDirection Error!");
        }
    }

    public void OnHit(float amount, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void noCollderReamin()
    {
        Flip();
    }
}
