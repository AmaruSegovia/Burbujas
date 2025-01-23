using UnityEngine;

public class Vodka : Bebida
{
    private float radioExplosion;
    private float fuerzaExplosion;
    private Transform playerTransform;

    public Vodka( float radio, float fuerza, Transform player)
    {
        this.radioExplosion = radio;
        this.fuerzaExplosion = fuerza;
        this.playerTransform = player;
    }

    public override string Nombre => "Vodka";


    public override void Activate()
    {
        Debug.Log("Oh no hermano");
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
    }
}
