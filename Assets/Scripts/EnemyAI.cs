using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Transform targetTransform;
    Mover _mover;
    Animator _animator;
    Rigidbody2D _rigidbody;
    bool isAttacking = false;
    Vector2 target;


    void Awake()
    {
        targetTransform = FindObjectOfType<Player>().transform;
        _mover = GetComponent<Mover>();
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(Attacking());    
    }

    void Update()
    {
        if (!isAttacking)
        {
            target = targetTransform.position;
            target.x += Mathf.Sign(transform.position.x-target.x);
            target.y += Mathf.Sign(transform.position.y-target.y)/2;
            transform.position = Vector2.MoveTowards(transform.position,target,2.5f*Time.deltaTime);

        if(targetTransform.position.x < transform.position.x && transform.localScale.x == 1f)
            { transform.localScale = new Vector2(-1f,transform.localScale.y); }
        else if (targetTransform.position.x > transform.position.x)
            { transform.localScale = new Vector2(1f,transform.localScale.y); }

        }
    }

    bool IsCloseToPlayer()
    {
        if(
            Mathf.Sign(transform.position.x) == Mathf.Sign(target.x) &&
            Mathf.Sign(transform.position.y) == Mathf.Sign(target.y) &&
            Mathf.Abs(transform.position.x - targetTransform.position.x) < 1.1f &&
            Mathf.Abs(transform.position.y - targetTransform.position.y) < 0.55f
        )   return true;
        return false;        
    }

    IEnumerator Attacking()
    {
        float randomDelay = 0;
        while(true)
        {
            while(!IsCloseToPlayer())
                { yield return new WaitForEndOfFrame(); }
            randomDelay = Random.Range(0.1f,0.4f);
            yield return new WaitForSeconds(randomDelay);
            isAttacking = true;
            for (int i = 0; i < 3; i++)
            {
                TryAttack();
                yield return new WaitForSeconds(0.33f);
            }
            randomDelay = Random.Range(0.4f,0.8f);
            yield return new WaitForSeconds(randomDelay);
            isAttacking = false;
            randomDelay = Random.Range(1f,3.5f);
            yield return new WaitForSeconds(randomDelay);
        }
    }

    void TryAttack()
    {
        string animatorBool = "pressedAttack";
        if (!_animator.GetBool(animatorBool))
        {
            StartCoroutine(ManageAnimatorBools(animatorBool));
        }
    }

    IEnumerator ManageAnimatorBools(string animatorBool)
    {
        _animator.SetBool(animatorBool,true);
        yield return new WaitForFixedUpdate();
        _animator.SetBool(animatorBool,false);
    }
}
