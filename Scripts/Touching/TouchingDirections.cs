using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于检测人物是否接触地面或者墙壁
public class TouchingDirections : MonoBehaviour
{
    CapsuleCollider2D coll;
    Animator anim;

    [SerializeField]
    private ContactFilter2D castFilter; //过滤器

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    public float groundDistance = 0.05f;
    public float wallDistance = 0.3f;
    public float ceilingDistance = 0.05f;


    [SerializeField]
    private bool _isGround;
    public bool IsGround {
        get {
            return _isGround;
        } 
        private set{
            _isGround = value;
            anim.SetBool(AnimationString.isGround, _isGround);
        }
    }

    [SerializeField]
    private bool _isWall;
    public bool IsWall {
        get {
            return _isWall;
        } 
        private set{
            _isWall = value;
            anim.SetBool(AnimationString.isWall, _isWall);
        }
    }

    [SerializeField]
    private bool _isCeiling;
    public bool IsCeiling {
        get {
            return _isCeiling;
        } 
        private set{
            _isCeiling = value;
            anim.SetBool(AnimationString.isCeiling, _isCeiling);
        }
    }

    public Vector2 WallDreiction => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    private void Awake() 
    {
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate() 
    {
        IsGround = coll.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsWall = coll.Cast(WallDreiction, castFilter, wallHits, wallDistance) > 0;
        IsCeiling = coll.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
