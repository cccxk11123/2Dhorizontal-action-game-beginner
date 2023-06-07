using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackDamage;
    public Vector2 knockback = new Vector2(5f, 2f);
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Damageable damageable = other.GetComponent<Damageable>();
        Vector2 knockbackDirection = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
        
        if(damageable != null && damageable.IsAlive)
        {
            bool isHit = damageable.Hit(attackDamage, knockbackDirection);
        }    
    }
}
