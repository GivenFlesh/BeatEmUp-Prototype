using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [Range(0.01f,2f)][SerializeField] float gravityScale = 0.5f;
    [Range(1,10)][SerializeField] float jumpMaxHeight = 4f;
    [Range(1,50)][SerializeField] float jumpInitialSpeed = 8f;
    bool canJump = true;
    float velocity = 0f;
    Vector3 initialPosition;
    Vector3 jumpTarget;

    void Awake()
    {
        initialPosition = transform.localPosition;
    }

    public void Jump()
    {
        if(canJump)
        {
            velocity = jumpInitialSpeed;
            jumpTarget = new Vector3 (initialPosition.x,initialPosition.y+jumpMaxHeight);
            StartCoroutine(ManageVerticalVelocity());
        }
    }

    public IEnumerator ManageVerticalVelocity()
    {
        canJump = false;
        do
        {
            Vector2 delta = new Vector2();
            delta.y = transform.localPosition.y + (velocity*Time.fixedDeltaTime);
            delta.y = Mathf.Clamp(delta.y,initialPosition.y,jumpTarget.y);
            Debug.Log(delta);
            transform.localPosition = delta;
            velocity = Mathf.Clamp(velocity - (gravityScale * 30f * Time.fixedDeltaTime),-40f,40);
            yield return new WaitForFixedUpdate();
        }
        while ( transform.localPosition != initialPosition );
        velocity = 0;
        canJump = true;
    }

}
