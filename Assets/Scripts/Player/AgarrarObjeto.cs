using UnityEngine;

public class AgarrarObjeto : MonoBehaviour
{
    public GameObject objeto;
    public Transform personaje;
    public float fuerza=25f;
    private bool activo=false;
    private Vector3 posicionInicialMouse;
    private Vector3 direccionLanzamiento;
    void Update()
    {
        if(activo)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                objeto.transform.SetParent(personaje);
                objeto.transform.position = personaje.position;
                //objeto.transform.rotation = personaje.rotation;
                Debug.Log("Agarrraste un objeto");
                //objeto.GetComponent<Rigidbody2D>().isKinematic = true;
                objeto.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
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

            if(Input.GetKeyDown(KeyCode.G))
            {
                objeto.transform.SetParent(null);
                //objeto.GetComponent<Rigidbody2D>().isKinematic = false;
                objeto.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            activo = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            activo = false;
        }
    }
}
