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
    //private Camera cam;
    /*void Start()
    {
        cam = Camera.main;
    }*/

     void Start()
    {
        agarrarObjeto = FindAnyObjectByType<AgarrarObjeto>();
    }
    void Update()
    {
        targetRotation = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg + 180;
        brazo.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        if(angle>90 || angle<-90){
            brazoSprite.flipY = true;
        }
        else{
            brazoSprite.flipY = false;
        }
        /*Vector2 mouseWorldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPoint - (Vector2)transform.position;
        transform.up = direction;*/
        if(Input.GetKeyDown(KeyCode.Mouse0) && agarrarObjeto.TieneObjeto()){
            Shoot();
        }
    }
    void Shoot(){
        elemento.transform.SetParent(null);
        elemento.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        targetRotation.z=0;
        finalTarget = (targetRotation - transform.position).normalized;
        elemento.GetComponent<Rigidbody2D>().AddForce(finalTarget * speed, ForceMode2D.Impulse);

        agarrarObjeto.TieneObjeto();
    }
    
}
