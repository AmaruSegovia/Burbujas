using UnityEngine;

public class PunchImpact : MonoBehaviour
{
    public float punchForce = 10f; // Fuerza del golpe

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Object")) // Filtrar los objetos afectados
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Calcular la dirección del golpe
                Vector3 punchDirection = other.transform.position - transform.position;
                punchDirection.Normalize();

                // Aplicar la fuerza en la dirección del golpe
                rb.AddForce(punchDirection * punchForce, ForceMode.Impulse);
            }
        }
    }


    void DeactivatePunch()
    {
        gameObject.SetActive(false);
    }
}
