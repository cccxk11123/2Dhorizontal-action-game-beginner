using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 10;
    public Vector3 spinRotation = new Vector3(0f, 180f, 0f);
    public AudioSource pickUpSound;

    private void Update() 
    {
        transform.eulerAngles += spinRotation * Time.deltaTime;    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if(damageable != null)
        {
            bool wasHeal = damageable.Heal(healthRestore);
            if(wasHeal)
            {
                AudioSource.PlayClipAtPoint(pickUpSound.clip, gameObject.transform.position, pickUpSound.volume);
                Destroy(gameObject);
            }
                
        }    
    }
}
