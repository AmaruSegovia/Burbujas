using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public ElevatorController elevator; // Referencia al ascensor
    public bool goUp; // Indica si este botón hace que el ascensor suba

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Llamado al ascensor");
            elevator.CallElevator(goUp);
        }
    }
}