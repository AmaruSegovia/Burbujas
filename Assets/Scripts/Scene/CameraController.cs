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

