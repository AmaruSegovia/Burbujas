using System.Collections;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Transform bottomPosition;
    public Transform topPosition;
    public float speed = 2f;
    public Animator doorAnimator;

    private bool isMoving = false;
    private Transform targetPosition;

    void Start()
    {
        transform.position = topPosition.position;
        targetPosition = topPosition;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveElevator();
        }
    }

    private void MoveElevator()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
        {
            isMoving = false;
            if (targetPosition == bottomPosition)
            {
                StartCoroutine(TurbulenceEffect());
            }
            else
            {
                doorAnimator.SetTrigger("abriendo");
            }
        }
    }

    public void CallElevator(bool goUp)
    {
        if (!isMoving)
        {
            StartCoroutine(OpenAndCloseDoor(goUp));
        }
    }

    private IEnumerator OpenAndCloseDoor(bool goUp)
    {
        doorAnimator.SetTrigger("abriendo");
        yield return new WaitForSeconds(2f); // Espera con la puerta abierta
        doorAnimator.SetTrigger("cerrando");
        yield return new WaitForSeconds(1f); // Espera para cerrar la puerta
        StartCoroutine(StartElevatorMovement(goUp));
    }

    private IEnumerator StartElevatorMovement(bool goUp)
    {
        targetPosition = goUp ? topPosition : bottomPosition;
        isMoving = true;
        yield return null;
    }

    private IEnumerator TurbulenceEffect()
    {
        Vector3 originalPosition = transform.position;
        float elapsedTime = 0f;
        float duration = 0.5f; // Duración de la turbulencia
        float magnitude = 0.1f; // Magnitud de la turbulencia

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition;
    }
}
