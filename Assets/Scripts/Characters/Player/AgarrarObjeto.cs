using System.Collections;
using UnityEngine;

public class AgarrarObjeto : MonoBehaviour
{
    //public GameObject objeto;
    public Transform personaje;
    public float fuerza=25f;
    public bool activo=false;
    private Vector3 posicionInicialMouse;
    private Vector3 direccionLanzamiento;
    public bool tieneObjeto = false;

    private Puntero puntero;

    private Collider2D objetoCollider;
    private Rigidbody2D rb;

   // public GameObject jugador;
    //private Collider2D jugadorCollider;

    void Start()
    {
        puntero = FindAnyObjectByType<Puntero>();
        rb = GetComponent<Rigidbody2D>();
        if (puntero == null)
        {
            Debug.LogWarning("No se ha encontrado el objeto Puntero.");
        }
    }
    public bool TieneObjeto()
    {
        return tieneObjeto;
    }
    void Update()
    {
        if(activo)
        {
            if(Input.GetKeyDown(KeyCode.E) && !tieneObjeto)
            {
                if (puntero != null)
                {
                    puntero.ActivarBrazo(true); // Activar brazo
                    puntero.SetObjetoActual(this); // Guardar referencia en el puntero
                }
                //objeto.transform.SetParent(personaje);
                //objeto.transform.position = personaje.position;
                transform.SetParent(personaje);
                transform.position = personaje.position;

                Debug.Log("Agarrraste un objeto");
                
                //objeto.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                rb.bodyType = RigidbodyType2D.Kinematic;

                tieneObjeto = true;
                //posicionInicialMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if(Input.GetKeyDown(KeyCode.G)  && tieneObjeto)
            {
                //objeto.transform.SetParent(null);
                transform.SetParent(null);
                
                //objeto.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                rb.bodyType = RigidbodyType2D.Dynamic;
                
                tieneObjeto = false;
                activo = false;
                if (puntero != null)
                {
                    puntero.ActivarBrazo(false); // Desactivar brazo
                    puntero.SetObjetoActual(null); // Limpiar referencia en el puntero
                }
            }
            /*if (tieneObjeto)
            {
                objeto.transform.position = personaje.position;
            }*/
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag ("Player"))
        {
            activo = true;
            //Physics2D.IgnoreCollision(objetoCollider, jugadorCollider, true);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("suelo") && !tieneObjeto)
        {
            Debug.Log("El objeto tocó el suelo.");
            
            //Rigidbody2D rb = objeto.GetComponent<Rigidbody2D>();
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            //objeto.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            if (rb != null && rb.bodyType == RigidbodyType2D.Static)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("El objeto ha salido del trigger del jugador.");
            activo = false;
        }
    }
}
