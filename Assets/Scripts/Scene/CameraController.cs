using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Referencia de la camara cinemachine")]
    [SerializeField] CinemachineCamera cameraLevel;
    [SerializeField] CinemachinePositionComposer composer;
    [SerializeField] CinemachineConfiner2D confiner;
    Transform playerTransform;

    private void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("CameraFollow"))
        {
            cameraLevel.Follow = playerTransform;
            BoxCollider2D childCollider = collision.GetComponentInChildren<BoxCollider2D>();
            if (childCollider != null)
            {
                confiner.BoundingShape2D = childCollider;
                composer.Composition.ScreenPosition = new Vector3(0f, 0.3f, 0);
            }
            else
            {
                Debug.LogWarning("No se encontró un BoxCollider2D en el objeto hijo.");
            }
        }
    }
}