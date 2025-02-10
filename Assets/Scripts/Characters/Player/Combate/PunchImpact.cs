using UnityEngine;

public class PunchImpact : MonoBehaviour
{
    public float punchForce = 10f; // Fuerza del golpe

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo") || collision.gameObject.CompareTag("Objeto")) // Filtrar los objetos afectados
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Calcular la dirección del golpe
                Vector3 punchDirection = collision.gameObject.transform.position - transform.position;
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
