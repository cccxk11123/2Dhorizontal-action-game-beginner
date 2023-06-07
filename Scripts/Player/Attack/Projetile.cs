using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetile : MonoBehaviour
{
    public float damage;
    public Vector2 moveSpeed = new Vector2(8f, 0f);
    public Vector2 knockback = new Vector2(3f, 0f);
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Damageable damageable = other.GetComponent<Damageable>();
        Vector2 knockbackDirection = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
        if(damageable != null && damageable.IsAlive)
        {
            bool isHit = damageable.Hit(damage, knockbackDirection);
            if(isHit)
                Destroy(gameObject);
        }
    }
}
