using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBoundsUpdater : MonoBehaviour
{
    Player _player;
    Jumper playerJumper;
    Vector3 starterPosition;
    [SerializeField] CinemachineVirtualCamera followCamController;
    CinemachineFramingTransposer cameraTransposer;
    float starterOffset;


    void Awake()
    {
        _player = FindObjectOfType<Player>();
        cameraTransposer = followCamController.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    void Start()
    {
        playerJumper = _player.GetComponentInChildren<Jumper>();
        starterPosition = playerJumper.transform.localPosition;
        starterOffset = cameraTransposer.m_TrackedObjectOffset.y;
    }

    public void UpdateCameraBounds(Vector3 newDistance)
    {
        transform.localPosition = starterPosition - newDistance;
        cameraTransposer.m_TrackedObjectOffset.y = starterOffset + newDistance.y;
    }
}
