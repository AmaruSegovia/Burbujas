using UnityEngine;

public class Pelea : MonoBehaviour, IGolpeable
{
    [SerializeField] Transform controladorGolpe;
    [SerializeField] float radioGolpe;
    [SerializeField] int danio;
    private Vector2 direccion;
    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            direccion = Vector2.right;
            DarGolpe(danio);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direccion = Vector2.left;
            DarGolpe(danio);
        }
    }
    public void DarGolpe(int danio)
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        foreach (Collider2D colisionador in objetos)
        {
            /*Hay que agregarle un tag al perro policia con el tag llamado Enemigo*/
            if (colisionador.CompareTag("Enemigo"))
            {
                /*Aqui se tendria que crear un script del enemigo para recibir el danio del golpe y que este acceda a su vida y le baje*/
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position + (Vector3)direccion, radioGolpe);
    }
}