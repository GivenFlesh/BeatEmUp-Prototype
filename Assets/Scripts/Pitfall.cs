using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitfall : MonoBehaviour
{
    [SerializeField] float respawnDistance = 2f;
    BoxCollider2D _collider;
    float respawnPointX;

    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        SetCollisionBounds();
        respawnPointX = transform.position.x - (transform.localScale.x/2) - respawnDistance;
    }

    void SetCollisionBounds()
    {
        Vector2 delta = transform.localScale;
        delta.x = (delta.x - 1.2f) / delta.x;
        delta.y = (delta.y - 0.6f) / delta.y;
        delta.x = Mathf.Clamp(delta.x, 0.01f, transform.localScale.x);
        delta.y = Mathf.Clamp(delta.y, 0.01f, transform.localScale.y);
        _collider.size = delta;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Mover otherMover = other.GetComponent<Mover>();
        if(otherMover != null)
        {
            otherMover.transform.position = new Vector2(respawnPointX,otherMover.transform.position.y);
            otherMover.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Jumper otherJumper = otherMover.GetComponentInChildren<Jumper>();
            otherJumper.transform.position = new Vector2(otherJumper.transform.position.x,10f);
            otherJumper.SetMaxHeight(11f);
            otherJumper.GetComponent<Health>().TakeDamage(5,0);
            Animator otherAnimator = otherJumper.GetComponent<Animator>();
            otherAnimator.SetBool("isFalling",true);
            otherAnimator.SetFloat("jumpMomentumX",0);
            otherAnimator.SetFloat("jumpMomentumY",0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawCube(new Vector2(respawnPointX,transform.position.y),new Vector2(1,transform.localScale.y));
    }
    
}
