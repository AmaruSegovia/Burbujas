using UnityEngine;

public class AgarrarObjeto : MonoBehaviour
{
    public GameObject objeto;
    public Transform personaje;
    public float fuerza=25f;
    private bool activo;
    void Update()
    {
        if(activo == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                objeto.transform.SetParent(personaje);
                objeto.transform.position = personaje.position;
                objeto.transform.rotation = personaje.rotation;
                Debug.Log("Agarrraste un objeto");
                //objeto.GetComponent<Rigidbody2D>().isKinematic = true;
                objeto.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
            if(Input.GetMouseButtonDown(1))
            {
                objeto.transform.SetParent(null);
                //objeto.GetComponent<Rigidbody2D>().isKinematic = false;
                objeto.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                objeto.GetComponent<Rigidbody2D>().AddForce(transform.right * fuerza, ForceMode2D.Impulse);
            }

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
