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

    public static CameraController instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Más de un CameraController en la escena");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerTransform = GetComponent<Transform>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CameraFollow"))
        {
            CameraSettings cameraSettings = collision.GetComponent<CameraSettings>();
            if (cameraSettings != null)
            {
                ApplyCameraSettings(cameraSettings);
            }
            else
            {
                Debug.LogWarning("No se encontró un componente CameraSettings en el objeto colisionado.");
            }
        }
    }
    private void ApplyCameraSettings(CameraSettings settings)
    {
        cameraLevel.Lens.OrthographicSize = settings.orthographicSize;
        composer.TargetOffset = settings.targetOffset;
        composer.Lookahead.IgnoreY = settings.ignoreY;
        confiner.BoundingShape2D = settings.boundingShape;
    }
    public void ShakeCamera(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}

