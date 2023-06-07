using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<float, Vector2> damageableHit;
    public UnityEvent<float, float> healthChanged;
    private Animator anim;

    [SerializeField]
    private bool isInvicible = false;
    private float invicibleTimeSince = 0f;

    [SerializeField]
    private float invicibleTime = 0.5f;

    [SerializeField]
    private bool isAlive = true;
    public bool IsAlive 
    { 
        get{ return isAlive; }
        private set
        {
            isAlive = value;
            anim.SetBool(AnimationString.isAlive, value);
        }    
    }

    [SerializeField]
    private float maxHealth = 100f;
    public float MaxHealth
    {
        get{ return maxHealth; } 
        private set{ maxHealth = value; }
    }

    [SerializeField]
    private float health = 100f;
    public float Health 
    {
        get{ return health; } 
        private set
        {
            health = value;
            healthChanged?.Invoke(health, maxHealth);
            if(health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    public bool LockVelocity
    {
        set { anim.SetBool(AnimationString.lockVelocity, value); }
        get { return anim.GetBool(AnimationString.lockVelocity); }
    }

    private void Awake() 
    {
        anim = GetComponent<Animator>();    
    }

    private void Update() 
    {
        if(isInvicible)
        {
            if(invicibleTimeSince >= invicibleTime)
            {
                isInvicible = false;
                invicibleTimeSince = 0f;
            }
            invicibleTimeSince += Time.deltaTime;
        }  
    }

    public bool Hit(float damage, Vector2 knockback)
    {
        if(isAlive && !isInvicible)
        {
            Health -= damage;
            isInvicible = true;
            LockVelocity = true;
            anim.SetTrigger(AnimationString.hit);
            CharacterEvent.charactertookDamage?.Invoke(this.gameObject, damage);
            damageableHit?.Invoke(damage, knockback); //击退效果
            return true;
        }
        return false;
    }

    public bool Heal(int amount)
    {
        if(isAlive && health < maxHealth)
        {
            float maxRestore = Mathf.Max(maxHealth - health, 0);
            float healRestore = Mathf.Min(maxRestore, amount);
            health += healRestore;
            CharacterEvent.characterHealed(this.gameObject, healRestore);
            return true;
        }
        return false;
    }
}
