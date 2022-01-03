using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Vector2 minBounds;
    Vector2 maxBounds;

    void Awake()
    {
        return;
    }

    void Start()
    {
        InitBounds();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));

    }

    public void Move(Rigidbody2D body, float speed, Vector2 direction)
    {
        Vector2 delta = direction * speed * (Time.fixedDeltaTime * 10f);
        
        body.velocity = delta;
    }

}
