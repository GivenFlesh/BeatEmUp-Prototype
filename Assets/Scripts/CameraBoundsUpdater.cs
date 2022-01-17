using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundsUpdater : MonoBehaviour
{
    Player _player;
    Jumper playerJumper;
    Vector3 starterPosition;

    void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    void Start()
    {
        playerJumper = _player.GetComponentInChildren<Jumper>();
        starterPosition = playerJumper.transform.localPosition;
    }

    void Update()
    {
        transform.localPosition = - playerJumper.groundPosition + starterPosition;
    }
}
