using UnityEngine;
using System.Collections;
public class InteraccionContenedor : MonoBehaviour
{
    //Verifica si el personaje está colisionando con el contenedor.
    private bool enContenedor = false;
    private AlcoholBar bubble;
    private Animator animator;
    public PlayerMovements movimiento;

    public SpriteRenderer tachoRenderer;
    public Sprite tachoVomitado;
    void Start(){
        animator = GetComponent<Animator>();
        bubble = FindAnyObjectByType<AlcoholBar>();
        movimiento = FindAnyObjectByType<PlayerMovements>();
    }

    //Compara los tags y verifica si el personaje está colisionando con el tag "Contenedor".
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Contenedor"))
        {
            enContenedor = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Contenedor"))
        {
            enContenedor = false;
        }
    }

    //Verifica si el booleano es verdadero y si se está presionando la tecla "E".
    void Update()
    {
        if (enContenedor && Input.GetKeyDown(KeyCode.E) && bubble.bubblesCant>1)
        {
            StartCoroutine(VaciarBurbujas());
            //animator.SetTrigger("Vomit");

            //bubble.PerderBubble();
            // Debug.Log("El personaje está interactuando con el contenedor.");
        }
    }

    private IEnumerator VaciarBurbujas()
    {
        movimiento.enabled = false;
        animator.SetTrigger("Vomit");

        while (bubble.bubblesCant > AlcoholBar.MinBubbles){
            bubble.PerderBubble();
            Debug.Log("El personaje está interactuando con el contenedor.");
            yield return new WaitForSeconds(0.25f);
        }
        
        yield return new WaitForSeconds(0.50f);
        tachoRenderer.sprite = tachoVomitado;

        movimiento.enabled = true;
    }
}
