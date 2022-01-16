using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisedGround : MonoBehaviour
{
    [SerializeField] float height = 0.75f;
    List<Jumper> objectsOnTop = new List<Jumper>();

    void OnTriggerEnter2D(Collider2D other)
    {
        Mover characterMover = other.GetComponent<Mover>();
        if(characterMover != null)
        {
            Jumper characterJumper = characterMover.GetComponentInChildren<Jumper>();
            objectsOnTop.Add(characterJumper);
            characterJumper.groundPosition.x += height;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        Mover characterMover = other.GetComponent<Mover>();
        if(characterMover != null)
        {
            Jumper characterJumper = characterMover.GetComponentInChildren<Jumper>();
            objectsOnTop.Add(characterJumper);
            characterJumper.groundPosition.x -= height;
        }
    }

    void OnDestroy()
    {
        foreach(Jumper jumper in objectsOnTop)
        {
            jumper.groundPosition.y -= height;
        }    
    }
}
