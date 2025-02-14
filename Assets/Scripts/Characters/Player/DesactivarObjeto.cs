using System.Collections;
using UnityEngine;

public class DesactivarObjeto : MonoBehaviour
{
    private bool colisionoConSuelo = false;
    public GameObject objeto;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.layer == LayerMask.NameToLayer("suelo"))
        {
            StartCoroutine(DesactivarElemento());
        }*/
        if (collision.gameObject.CompareTag("suelo") && !colisionoConSuelo)
        {
            colisionoConSuelo = true;
            Debug.Log("Colisionó con el suelo. Se desactivará en 5 segundos.");
            StartCoroutine(DesactivarElemento());
        }
    }
    private IEnumerator DesactivarElemento()
    {
        yield return new WaitForSeconds(5f);
        objeto.SetActive(false);
    }
}
