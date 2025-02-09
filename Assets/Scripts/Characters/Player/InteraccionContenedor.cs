using UnityEngine;
using System.Collections;
public class InteraccionContenedor : MonoBehaviour
{
    //Verifica si el personaje está colisionando con el contenedor.
    private bool enContenedor = false;
    //private AlcoholBar bubble;
    private ScriptGameManager alcohol;
    private Animator animator;
    public PlayerMovements movimiento;

    //public SpriteRenderer tachoRenderer;
    //public Sprite tachoVomitado;
    private Tacho tachoActual;
    void Start(){
        animator = GetComponent<Animator>();
        //bubble = FindAnyObjectByType<AlcoholBar>();
        alcohol = FindAnyObjectByType<ScriptGameManager>();
        movimiento = FindAnyObjectByType<PlayerMovements>();
    }

    //Compara los tags y verifica si el personaje está colisionando con el tag "Contenedor".
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Contenedor"))
        {
            enContenedor = true;
            tachoActual = other.GetComponent<Tacho>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Contenedor"))
        {
            enContenedor = false;
            tachoActual = null;
        }
    }

    //Verifica si el booleano es verdadero y si se está presionando la tecla "E".
    void Update()
    {
        if (enContenedor && Input.GetKeyDown(KeyCode.E) && ScriptGameManager.instance.PuntosTotalAlcohol > 0)
        {
            StartCoroutine(VaciarBurbujas());
            //animator.SetTrigger("Vomit");

            //bubble.PerderBubble();
            // Debug.Log("El personaje está interactuando con el contenedor.");
        }
    }

    private IEnumerator VaciarBurbujas()
    {
        if (tachoActual == null) yield break;

        movimiento.enabled = false;
        

        /*while (bubble.bubblesCant > AlcoholBar.MinBubbles){
            bubble.PerderBubble();
            Debug.Log("El personaje está interactuando con el contenedor.");
            yield return new WaitForSeconds(0.25f);
        }*/
        //alcohol.QuitarAlcohol(0.5f, 1.5f);
        ScriptGameManager.instance.QuitarAlcohol(0.5f, 1.5f);
        animator.SetTrigger("Vomit");
        //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(2f);
        //tachoRenderer.sprite = tachoVomitado;
        tachoActual.LlenarTacho();

        movimiento.enabled = true;
    }
}
