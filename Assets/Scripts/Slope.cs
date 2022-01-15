using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slope : MonoBehaviour
{
    [SerializeField] float slopeValue = 10f;
    [SerializeField] float slopeHeight = 4f;

    BoxCollider2D _collider;
    float minXBounds;
    float maxXBounds;
    float slopeLength;
    int slopeClimbDirection;
    
    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        slopeClimbDirection = (int)-Mathf.Sign(slopeValue);
        if(slopeClimbDirection == 1)
        {
            minXBounds = _collider.bounds.min.x-0.5f;
            maxXBounds = _collider.bounds.max.x+0.5f;
        }
        else
        {
            minXBounds = _collider.bounds.max.x+0.5f;
            maxXBounds = _collider.bounds.min.x-0.5f;
        }
        slopeLength = Mathf.Abs(maxXBounds - minXBounds);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Mover otherMover = other.GetComponent<Mover>();
        if (otherMover != null)
        {
            otherMover.onSlope = true;
            StartCoroutine(otherMover.SlopeMoveX(slopeValue/100f));
        }
        Jumper otherJumper = other.GetComponent<Jumper>();
        if (otherJumper != null)
        {
            otherJumper.slopeVariance = slopeValue;
        }
        if(otherJumper != null && otherMover != null && !otherJumper.isOnSlope)
        {
            // StartCoroutine(SlopeClimb(otherJumper));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Mover otherMover = other.GetComponent<Mover>();
        if (otherMover != null)
        {
            otherMover.onSlope = false;
        }
        Jumper otherJumper = other.GetComponent<Jumper>();
        if (otherJumper != null)
        {
            otherJumper.slopeVariance = 0f;
        }    }

    // IEnumerator SlopeClimb(Jumper characterJumper)
    // {
    //     characterJumper.isOnSlope = true;
    //     Vector2 originalPosition = characterJumper.initialPosition;
    //     Transform characterTransform = characterJumper.transform.parent;
    //     Vector2 exitLeft = 
    //     float center = minXBounds + (slopeLength/2);
    //     int enteredDirection = (int)Mathf.Sign(Mathf.Abs(characterTransform.position.x) - Mathf.Abs(center));
    //     while(characterJumper.isOnSlope)
    //     {
    //         float delta = Mathf.Abs(characterTransform.position.x-maxXBounds);
    //         delta /= slopeLength;
    //         characterJumper.initialPosition.y = originalPosition.y + (slopeHeight * delta);
    //         yield return new WaitForEndOfFrame();
    //     }
    //     int exitDirection = (int)Mathf.Sign(Mathf.Abs(characterTransform.position.x) - Mathf.Abs(center));
    //     if (exitDirection == slopeClimbDirection)
    //     {
    //         characterJumper.initialPosition.y = originalPosition.y + slopeHeight;
    //     }
    //     else
    //     {
    //         characterJumper.initialPosition.y = originalPosition.y - slopeHeight;
    //     }

    // }
}
