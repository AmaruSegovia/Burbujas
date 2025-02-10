using System.Collections;
using UnityEngine;

public class PunchImpact : MonoBehaviour
{
    public float punchForce = 10f; // Fuerza del golpe
    public Sprite manoRotaSprite; // Sprite de la mano rota
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    private Sprite manoNormalSprite; // Sprite de la mano normal

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        manoNormalSprite = spriteRenderer.sprite; // Guardar el sprite original de la mano
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstaculo")) // Filtrar los objetos afectados
        {
            RomperObstaculo(collision.gameObject);
        }
        else if (collision.CompareTag("ObstaculoDuro"))
        {
            if (ScriptGameManager.instance.fuerza >= 2) // Verificar la fuerza del jugador
            {
                RomperObstaculo(collision.gameObject);
            }
            else
            {
                // Cambiar el sprite de la mano a una mano rota
                spriteRenderer.sprite = manoRotaSprite;
                // Aumentar ligeramente la barra de alcohol
                ScriptGameManager.instance.ActualizarAlcohol(5f); // Ajusta este valor según la cantidad de alcohol que desees aumentar
                // Iniciar la corrutina para restaurar el sprite y temblar la cámara
                StartCoroutine(RestoreHandAndShakeCamera());
            }
        }
        else if (collision.CompareTag("ElevatorDoor"))
        {
            if (ScriptGameManager.instance.fuerza >= 2) // Verificar la fuerza del jugador
            {
                ElevatorDoor door = collision.GetComponent<ElevatorDoor>();
                if (door != null)
                {
                    // Calcular la dirección del golpe
                    Vector3 punchDirection = collision.transform.position - transform.position;
                    punchDirection.Normalize();
                    // Añadir una componente vertical a la dirección del golpe
                    punchDirection.y = 2f; // Ajusta este valor según la cantidad de fuerza vertical que desees
                    // Aplicar la fuerza en la dirección del golpe
                    door.ApplyForce(punchDirection * punchForce);
                }
            }
            else
            {
                // Cambiar el sprite de la mano a una mano rota
                spriteRenderer.sprite = manoRotaSprite;
                // Aumentar ligeramente la barra de alcohol
                ScriptGameManager.instance.ActualizarAlcohol(5f); // Ajusta este valor según la cantidad de alcohol que desees aumentar
                // Iniciar la corrutina para restaurar el sprite y temblar la cámara
                StartCoroutine(RestoreHandAndShakeCamera());
            }
        }
    }

    private void RomperObstaculo(GameObject obstaculo)
    {
        Rigidbody2D rb = obstaculo.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Calcular la dirección del golpe
            Vector3 punchDirection = obstaculo.transform.position - transform.position;
            punchDirection.Normalize();
            // Añadir una componente vertical a la dirección del golpe
            punchDirection.y = 2f; // Ajusta este valor según la cantidad de fuerza vertical que desees
            // Aplicar la fuerza en la dirección del golpe
            rb.AddForce(punchDirection * punchForce, ForceMode2D.Impulse);

            // Destruir el objeto después de 0.3 segundos
            Destroy(obstaculo, 0.3f);
        }
    }

    private IEnumerator RestoreHandAndShakeCamera()
    {
        // Temblar la cámara
        CameraController.instance.ShakeCamera(0.2f, 0.1f); // Ajusta estos valores según la intensidad y duración del temblor

        // Esperar 1 segundo antes de restaurar el sprite
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = manoNormalSprite;
    }

    void DeactivatePunch()
    {
        gameObject.SetActive(false);
    }
}
