using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    [Tooltip("Enable tiled texture and triple size for best effect")]
    [SerializeField] bool infiniteScrollEnabled = true;
    [Header("Autoscroll")]
    [SerializeField] float autoScrollSpeedHorizontal = 0f;
    [SerializeField] float autoScrollSpeedVertical = 0f;
    [Header("Parallax Proportion")]
    [SerializeField] Transform followTarget;
    [SerializeField] float parallaxScrollHorizontal = 0f;
    [SerializeField] float parallaxScrollVertical = 0f;

    Vector3 lastTargetPosition;
    float textureUnitSizeY;
    float textureUnitSizeX;
    Camera mainCamera;

    void Awake()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        mainCamera = Camera.main;
    }

    void Start()
    {
        if(followTarget == null) { followTarget = mainCamera.transform; }
        lastTargetPosition = followTarget.position;
    }

    void LateUpdate()
    {
        ParallaxScroll();
        AutoScrollScreen();
        InfiniteScroll();
    }

    private void ParallaxScroll()
    {
        if(followTarget != null)
        {
            Vector2 delta = followTarget.position - lastTargetPosition;
            transform.position += new Vector3 (delta.x * parallaxScrollHorizontal,delta.y * parallaxScrollVertical);
            lastTargetPosition = followTarget.position;
        }
    }

    void AutoScrollScreen()
    {
        Vector2 delta = new Vector2 (autoScrollSpeedHorizontal * Time.deltaTime,autoScrollSpeedVertical * Time.deltaTime);
        transform.position -= (Vector3)delta;
    }

    private void InfiniteScroll()
    {
        if (Mathf.Abs(mainCamera.transform.position.y - transform.position.y) >= textureUnitSizeY && infiniteScrollEnabled)
        {
            float offsetPositionY = (mainCamera.transform.position.y - transform.position.y) % textureUnitSizeY;
            transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y + offsetPositionY);
        }
        if (Mathf.Abs(mainCamera.transform.position.x - transform.position.x) >= textureUnitSizeX && infiniteScrollEnabled)
        {
            float offsetPositionX = (mainCamera.transform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(mainCamera.transform.position.x + offsetPositionX, transform.position.y);
        }
    }
}
