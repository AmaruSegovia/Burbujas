using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
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
    public Volume globalVolume;
    private DepthOfField depth;
    private LensDistortion distortion;
    private Bloom bloom;

    //public Contador contador;
    void Start(){
        animator = GetComponent<Animator>();
        //bubble = FindAnyObjectByType<AlcoholBar>();
        alcohol = FindAnyObjectByType<ScriptGameManager>();
        movimiento = FindAnyObjectByType<PlayerMovements>();
        globalVolume.gameObject.SetActive(false);

        //bool estado = contador.GetIsCountdownActive();

        if(globalVolume.profile.TryGet(out depth)){
            depth.active = false;
        }
        if(globalVolume.profile.TryGet(out distortion)){
            distortion.active = false;
        }
        if(globalVolume.profile.TryGet(out bloom)){
            bloom.active = false;
        }
    }

    //Compara los tags y verifica si el personaje está colisionando con el tag "Contenedor".
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Contenedor"))
        {
            enContenedor = true;
            tachoActual = other.GetComponent<Tacho>();
            Debug.Log("Dentro");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Contenedor"))
        {
            enContenedor = false;
            Debug.Log("Fuera");
            tachoActual = null;
        }
    }

    //Verifica si el booleano es verdadero y si se está presionando la tecla "E".
    void Update()
    {
        if(ScriptGameManager.instance.PuntosTotalAlcohol>=50){
            globalVolume.gameObject.SetActive(true);
        }
        /*else if(ScriptGameManager.instance.PuntosTotalAlcohol>=65){
            depth.active = true;
        }*/
        else{
            globalVolume.gameObject.SetActive(false);
        }

        if (ScriptGameManager.instance.PuntosTotalAlcohol >= 75)
        {
            depth.active = true;
        }
        else
        {
            depth.active = false;
        }

        if(ScriptGameManager.instance.PuntosTotalAlcohol == 100){
            distortion.active = true;
            bloom.active = true;
        }
        else{
            distortion.active = false;
            bloom.active = false;
        }

        if (enContenedor && Input.GetKeyDown(KeyCode.E) && ScriptGameManager.instance.PuntosTotalAlcohol > 0)
        {
            StartCoroutine(VaciarBurbujas());
            //contador.SetIsCountdownActive(false);
            //animator.SetTrigger("Vomit");

            //bubble.PerderBubble();
            Debug.Log("El personaje está interactuando con el contenedor.");
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
        // Llamar al método para quitar todo el alcohol
        ScriptGameManager.instance.QuitarAlcohol(0.5f, 1.5f);
        Contador.instance.DetenerYParpadearContador(5);
        animator.SetTrigger("Vomit");
        //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(2f);
        //tachoRenderer.sprite = tachoVomitado;
        tachoActual.LlenarTacho();

        movimiento.enabled = true;
    }
}
