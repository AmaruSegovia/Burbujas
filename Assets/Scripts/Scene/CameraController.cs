using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Referencia de la camara cinemachine")]
    [SerializeField] CinemachineCamera cameraLevel;
    [SerializeField] CinemachinePositionComposer composer;
    Transform playerTransform;

    private void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CameraPoint"))
        {
            cameraLevel.Follow = collision.transform;
            composer.Composition.ScreenPosition = new Vector3(0f, 0f, 0);
        }

        if (collision.CompareTag("CameraFollow"))
        {
            cameraLevel.Follow = playerTransform;
            composer.Composition.ScreenPosition = new Vector3(0f, 0.3f, 0);
        }
    }
}