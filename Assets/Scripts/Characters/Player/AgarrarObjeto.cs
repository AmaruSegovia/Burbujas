using UnityEngine;

public class AgarrarObjeto : MonoBehaviour
{
    public GameObject objeto;
    public Transform personaje;
    public float fuerza=25f;
    private bool activo=false;
    private Vector3 posicionInicialMouse;
    private Vector3 direccionLanzamiento;
    private bool tieneObjeto = false;

    private Puntero puntero;

    private Collider2D objetoCollider;

   // public GameObject jugador;
    //private Collider2D jugadorCollider;

    void Start()
    {
        puntero = FindAnyObjectByType<Puntero>();
        if (puntero == null)
        {
            Debug.LogWarning("No se ha encontrado el objeto Puntero.");
        }
        //objetoCollider = objeto.GetComponent<Collider2D>();
        //jugadorCollider = FindAnyObjectByType<Player>().GetComponent<Collider2D>();
       // jugadorCollider = jugador.GetComponent<Collider2D>();
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
                }
                objeto.transform.SetParent(personaje);
                objeto.transform.position = personaje.position;
                //objeto.transform.rotation = personaje.rotation;
                Debug.Log("Agarrraste un objeto");
                //objeto.GetComponent<Rigidbody2D>().isKinematic = true;
                objeto.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                tieneObjeto = true;
                //posicionInicialMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            /*if (Input.GetMouseButton(1))
            {
                Vector3 posicionActualMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direccionLanzamiento = (posicionActualMouse - posicionInicialMouse).normalized;
            }
            if(Input.GetMouseButtonUp(1))
            {
                objeto.transform.SetParent(null);
                //objeto.GetComponent<Rigidbody2D>().isKinematic = false;
                objeto.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                //objeto.GetComponent<Rigidbody2D>().AddForce(transform.right * fuerza, ForceMode2D.Impulse);
                objeto.GetComponent<Rigidbody2D>().AddForce(direccionLanzamiento * fuerza, ForceMode2D.Impulse);
            }*/

            if(Input.GetKeyDown(KeyCode.G)  && tieneObjeto)
            {
                objeto.transform.SetParent(null);
                //objeto.GetComponent<Rigidbody2D>().isKinematic = false;
                objeto.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                tieneObjeto = false;
                if (puntero != null)
                {
                    puntero.ActivarBrazo(false); // Desactivar brazo
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag ("Player"))
        {
            activo = true;
            //Physics2D.IgnoreCollision(objetoCollider, jugadorCollider, true);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("suelo"))
        {
            Debug.Log("El objeto tocó el suelo.");
            
            Rigidbody2D rb = objeto.GetComponent<Rigidbody2D>();
            //objeto.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            if (rb.bodyType == RigidbodyType2D.Static)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            //rb.linearVelocity = Vector2.zero;
           // objeto.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
           
           //rb.bodyType = RigidbodyType2D.Static;
           Collider2D objetoCollider = objeto.GetComponent<Collider2D>();
           if (objetoCollider.isTrigger)
            {
                objetoCollider.isTrigger = false;  // Dejar de ser Trigger
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            activo = false;
             //Physics2D.IgnoreCollision(objetoCollider, jugadorCollider, false);
        }
    }
}
