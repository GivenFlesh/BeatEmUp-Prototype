using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slope : MonoBehaviour
{
    [SerializeField] float slopeValue = 10f;

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

}
