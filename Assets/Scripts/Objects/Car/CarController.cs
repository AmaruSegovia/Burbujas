using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform startPoint; // Punto inicial
    public Transform endPoint; // Punto final
    public float minSpeed = 3f; // Velocidad mínima del auto
    public float maxSpeed = 7f; // Velocidad máxima del auto
    public float startDelay = 2f; // Tiempo de inicio del movimiento

    private float speed; // Velocidad actual del auto
    private bool isMoving = false;

    private void Start()
    {
        // Colocar el auto en el punto inicial al inicio
        transform.position = startPoint.position;
        // Iniciar la corrutina para comenzar el movimiento después del tiempo de inicio
        StartCoroutine(StartMovement());
    }

    private IEnumerator StartMovement()
    {
        // Esperar el tiempo de inicio del movimiento
        yield return new WaitForSeconds(startDelay);
        // Establecer la velocidad aleatoria
        speed = Random.Range(minSpeed, maxSpeed);
        // Activar el movimiento del auto
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveCar();
        }
    }

    private void MoveCar()
    {
        // Mover el auto hacia el punto final
        transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);

        // Verificar si el auto ha llegado al punto final
        if (Vector3.Distance(transform.position, endPoint.position) < 0.01f)
        {
            isMoving = false;
            StartCoroutine(TeleportCar());
        }
    }

    private IEnumerator TeleportCar()
    {
        // Esperar un tiempo aleatorio entre 1 y 3 segundos
        float waitTime = Random.Range(1f, 3f);
        yield return new WaitForSeconds(waitTime);

        // Teletransportar el auto al punto inicial
        transform.position = startPoint.position;

        // Establecer la velocidad aleatoria
        speed = Random.Range(minSpeed, maxSpeed);

        // Reactivar el movimiento del auto
        isMoving = true;
    }
}


