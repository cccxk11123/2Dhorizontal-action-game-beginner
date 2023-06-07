using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCollderReamin;
    private Animator anim;
    private Collider2D coll;
    public List<Collider2D> detectionCollders = new List<Collider2D>();

    private void Awake() 
    {
        coll = GetComponentInChildren<Collider2D>();
        anim = GetComponent<Animator>();    
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        detectionCollders.Add(other);
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        detectionCollders.Remove(other);
        if(detectionCollders.Count <= 0)
        {
            noCollderReamin?.Invoke();
        }
    }
}
