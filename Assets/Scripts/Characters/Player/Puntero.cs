using System.Collections;
using UnityEngine;

public class Puntero : MonoBehaviour
{
    public Transform brazo;
    public SpriteRenderer brazoSprite;
    Vector3 targetRotation;

    public int speed;
    public GameObject elemento;
    Vector3 finalTarget;
    public Transform mano;
     private AgarrarObjeto agarrarObjeto;

     public SpriteRenderer personajeSprite;
     public Transform personaje;
    //private Camera cam;
    /*void Start()
    {
        cam = Camera.main;
    }*/

     void Start()
    {
        agarrarObjeto = FindAnyObjectByType<AgarrarObjeto>();
        brazoSprite.enabled = false;
    }
    void Update()
    {
        if (!brazoSprite.enabled) return;

        brazo.position = personaje.position;

        //targetRotation = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        targetRotation = Input.mousePosition - Camera.main.WorldToScreenPoint(brazo.position);
        var angle = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg + 180;
        //brazo.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //brazo.rotation = Quaternion.Euler(0, 0, angle);
        
        if (personajeSprite.flipX)
        {
            brazo.rotation = Quaternion.Euler(0, 0, angle + 180);
            brazoSprite.flipY = true;
        }
        else
        {
            brazo.rotation = Quaternion.Euler(0, 0, angle);
            brazoSprite.flipY = false;
        }

        /*if(angle>90 || angle<-90){
            brazoSprite.flipY = true;
        }
        else{
            brazoSprite.flipY = false;
        }*/
        /*Vector2 mouseWorldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPoint - (Vector2)transform.position;
        transform.up = direction;*/
        if(Input.GetKeyDown(KeyCode.F) && agarrarObjeto.TieneObjeto()){
            Shoot();
            agarrarObjeto.activo = false;
        }
    }
    void Shoot(){
        elemento.transform.SetParent(null);
        elemento.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        targetRotation.z=0;
        finalTarget = (targetRotation - transform.position).normalized;
        elemento.GetComponent<Rigidbody2D>().AddForce(finalTarget * speed, ForceMode2D.Impulse);

        agarrarObjeto.tieneObjeto = false;
        
        //agarrarObjeto.activo = false;

        //StartCoroutine(DesactivarTrigger(elemento));
        StartCoroutine(ReactivarCollider(elemento));
        //agarrarObjeto.tieneObjeto = false;
        brazoSprite.enabled = false;
    }
    public void ActivarBrazo(bool estado)
    {
        brazoSprite.enabled = estado;
    }
    
    /*private IEnumerator DesactivarTrigger(GameObject objeto)
    {
        yield return new WaitForSeconds(0.2f);
        Collider2D objetoCollider = objeto.GetComponent<Collider2D>();
        if (objetoCollider != null) {
            objetoCollider.isTrigger = false;
        }
    }*/
    private IEnumerator ReactivarCollider(GameObject objeto)
    {
        // Desactiva el collider para forzar OnTriggerExit2D
        Collider2D objetoCollider = objeto.GetComponent<Collider2D>();
        if (objetoCollider != null)
        {
            objetoCollider.enabled = false;
        }
        yield return new WaitForSeconds(0.2f);
        // Reactiva el collider como no-trigger para que colisione con el entorno
        if (objetoCollider != null)
        {
            objetoCollider.isTrigger = false;
            objetoCollider.enabled = true;
        }
    }
}
