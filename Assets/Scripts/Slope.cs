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
        if(otherMover != null)
        {
            Jumper slopedJumper = otherMover.GetComponentInChildren<Jumper>();
            if(!slopedJumper.isOnSlope)
                StartCoroutine(SlopeClimb(slopedJumper));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Mover otherMover = other.GetComponent<Mover>();
        if (otherMover != null)
        {
            otherMover.onSlope = false;
            otherMover.GetComponentInChildren<Jumper>().isOnSlope = false;
        }
        Jumper otherJumper = other.GetComponent<Jumper>();
        if (otherJumper != null)
        {
            otherJumper.slopeVariance = 0f;
        }    
    }

    IEnumerator SlopeClimb(Jumper characterJumper)
    {
        characterJumper.isOnSlope = true;
        Vector2 originalPosition = characterJumper.initialPosition;
        Transform characterTransform = characterJumper.transform.parent;
        float center = minXBounds + (slopeLength/2);
        int enteredDirection = (int)Mathf.Sign(Mathf.Abs(characterTransform.position.x) - Mathf.Abs(center));
        Vector2 exitLeft = new Vector2 ( 0f,
            -1f * ((-1f - (float)enteredDirection) / 2f) * slopeHeight * (float)slopeClimbDirection * (float)enteredDirection + originalPosition.y);
        Vector2 exitRight = new Vector2 ( 0f,
            -1f * ((1f - (float)enteredDirection) / 2f) * slopeHeight * (float)slopeClimbDirection * (float)enteredDirection + originalPosition.y);
        while(characterJumper.isOnSlope)
        {
            float delta = Mathf.Abs(maxXBounds-characterTransform.position.x);
            delta /= slopeLength;
            delta = 1 - delta;
            characterJumper.initialPosition.y = originalPosition.y + (slopeHeight * delta);
            yield return new WaitForEndOfFrame();
        }
        int exitDirection = (int)Mathf.Sign(Mathf.Abs(characterTransform.position.x) - Mathf.Abs(center));
        if (exitDirection == 1)
        {
            characterJumper.initialPosition = exitRight;
        }
        else
        {
            characterJumper.initialPosition = exitLeft;
        }
    }
}
