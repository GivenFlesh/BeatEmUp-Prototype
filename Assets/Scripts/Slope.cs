using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slope : MonoBehaviour
{
    [SerializeField] float slopeHeight = 4f;

    BoxCollider2D _collider;
    float minXBounds;
    float maxXBounds;
    float slopeLength;
    int slopeClimbDirection;
    float slopeAngle;
    CameraBoundsUpdater playerFollow;

    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        playerFollow = Camera.main.GetComponentInChildren<CameraBoundsUpdater>();
    }

    void Start()
    {
        slopeClimbDirection = (int)-Mathf.Sign(transform.localScale.x);
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
        slopeAngle =((1 - slopeClimbDirection) / 2 * 360) + (Mathf.Atan2(slopeHeight,slopeLength-1f) * Mathf.Rad2Deg) * slopeClimbDirection;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Mover otherMover = other.GetComponent<Mover>();
        if (otherMover != null)
        {
            otherMover.groundAngle = slopeAngle;
            StartCoroutine(otherMover.SlopeMoveX(180-slopeAngle));
            Jumper otherJumper = otherMover.GetComponentInChildren<Jumper>();
            if (otherJumper != null)
            {
                otherJumper.slopeAngle = slopeAngle;
                if(!otherJumper.isOnSlope)
                    StartCoroutine(SlopeClimb(otherJumper));
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Mover otherMover = other.GetComponent<Mover>();
        if (otherMover != null)
        {
            otherMover.groundAngle = 0;
            Jumper otherJumper = otherMover.GetComponentInChildren<Jumper>();
            if (otherJumper != null)
            {
                otherJumper.isOnSlope = false;
                otherJumper.slopeAngle = 0f;
            }    
        }
    }

    IEnumerator SlopeClimb(Jumper characterJumper)
    {
        Player player = characterJumper.transform.parent.GetComponent<Player>();
        characterJumper.isOnSlope = true;
        Vector2 originalPosition = characterJumper.groundPosition;
        Transform characterTransform = characterJumper.transform.parent;
        float center = _collider.bounds.min.x-0.5f + (slopeLength/2);
        int enteredDirection = (int)Mathf.Sign(characterTransform.position.x - center);
        Vector2 exitLeft = new Vector2 ( 0f,
            -1f * ((-1f - (float)enteredDirection) / -2f) * slopeHeight * (float)slopeClimbDirection * (float)enteredDirection + originalPosition.y);
        Vector2 exitRight = new Vector2 ( 0f,
            -1f * ((1f - (float)enteredDirection) / 2f) * slopeHeight * (float)slopeClimbDirection * (float)enteredDirection + originalPosition.y);
        while(characterJumper.isOnSlope)
        {
            float delta = Mathf.Abs(maxXBounds-characterTransform.position.x);
            delta /= slopeLength;
            delta = (((-1 - enteredDirection * -slopeClimbDirection) / -2) - delta);
            characterJumper.groundPosition.y = originalPosition.y + (slopeHeight * delta);
            if (player != null)
                playerFollow.UpdateCameraBounds(characterJumper.groundPosition);
            yield return new WaitForEndOfFrame();
        }
        int exitDirection = (int)Mathf.Sign(characterTransform.position.x - center);
        if (exitDirection == 1)
        {
            characterJumper.groundPosition = exitRight;
            if (player != null)
                playerFollow.UpdateCameraBounds(characterJumper.groundPosition);
        }
        else
        {
            characterJumper.groundPosition = exitLeft;
            if (player != null)
                playerFollow.UpdateCameraBounds(characterJumper.groundPosition);        }
    }
}
