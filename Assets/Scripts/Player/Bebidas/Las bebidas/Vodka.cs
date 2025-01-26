using System.Collections;
using UnityEngine;

public class Vodka : Bebida
{
    private float radioExplosion;
    private float fuerzaExplosion;
    private Transform playerTransform;
    private ParticleSystem sistemaParticulasExplosion;
    public Vodka(float radio, float fuerza, Transform player, ParticleSystem sistemaParticulasExplosion)
    {
        this.radioExplosion = radio;
        this.fuerzaExplosion = fuerza;
        this.playerTransform = player;
        this.sistemaParticulasExplosion = sistemaParticulasExplosion;
    }

    public override string Nombre => "Vodka";
    public override string AnimTrigger => "BeberVodka";

    public override void Activate()
    {
        Debug.Log("Oh no hermano");

        playerTransform.GetComponent<MonoBehaviour>().StartCoroutine(DelayExplosion());
    }
    public override void alcoholDrink(){
        AlcoholBar.Instance.beber(2);
    }

    private IEnumerator DelayExplosion()
    {
        yield return new WaitForSeconds(2f);
        Explosion(); 
    }



    private void Explosion()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(playerTransform.position, radioExplosion);
        foreach (Collider2D colisionador in objetos)
        {
            Rigidbody2D rb2D = colisionador.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                Vector2 direccion = colisionador.transform.position - playerTransform.position;
                float distancia = 1 + direccion.magnitude;
                float fuerzaFinal = fuerzaExplosion / distancia;
                rb2D.AddForce(direccion * fuerzaFinal);
            }
        }
        sistemaParticulasExplosion.Play();
    }
}

