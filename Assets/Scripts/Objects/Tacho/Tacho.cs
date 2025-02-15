using UnityEngine;

public class Tacho : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D tachoCollider;
    public Sprite tachoVomitado;
    private bool usado = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tachoCollider = GetComponent<Collider2D>();
    }

    public void LlenarTacho()
    {
        if (!usado)
        {
            spriteRenderer.sprite = tachoVomitado;
            tachoCollider.enabled = false; 
            usado = true; 

        }
    }
    public void OcultarSprite()
    {
        spriteRenderer.enabled = false;
    }
    public void MostrarSprite()
    {
        spriteRenderer.enabled = true;
    }
    /*public void Reaparecer()
    {   
        gameObject.SetActive(true);
        tachoCollider.enabled = true;
    }*/
}
