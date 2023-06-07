using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Damageable damageable;
    public DetectionZone enmeyZone;
    public List<Transform> wayPoints;
    private Transform nextWayPoint;
    private int wayIndexNum = 0;
    private float wayPointReachedDistance = 0.1f;

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

    public bool CanMove 
    { 
        get { return anim.GetBool(AnimationString.canMove);} 
        private set{ }
    }

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
        damageable = GetComponent<Damageable>();
    }

    private void Start() 
    {
        nextWayPoint = wayPoints[wayIndexNum];    
    }

    private void Update() 
    {
        HasTarget = enmeyZone.detectionCollders.Count > 0;
    }

    private void FixedUpdate()
    {
        if(damageable.IsAlive)
        {
            if(CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            rb.gravityScale = 2f;
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    private void Flight()
    {
        Vector2 flightDirection = (nextWayPoint.position - transform.position).normalized;
        float distance = Vector2.Distance(nextWayPoint.position, transform.position);
        rb.velocity = flightDirection * distance;
        
        UpdateDirection();

        if(distance <= wayPointReachedDistance)
        {
            wayIndexNum = (wayIndexNum + 1) % wayPoints.Count;
            nextWayPoint = wayPoints[wayIndexNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;
        if(transform.localScale.x > 0)
        {
            if(rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
            }
        }
        else
        {
            if(rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
            }
        }
    }

    public void OnHit(float amount, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
