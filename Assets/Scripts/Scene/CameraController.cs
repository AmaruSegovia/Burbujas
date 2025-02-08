using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Referencia de la camara cinemachine")]
    [SerializeField] CinemachineCamera cameraLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CameraPoint"))
        {
            cameraLevel.Follow = collision.transform;
        }
    }
}