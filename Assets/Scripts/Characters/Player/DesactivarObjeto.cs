using System.Collections;
using UnityEngine;

public class DesactivarObjeto : MonoBehaviour
{
    public GameObject objeto;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("suelo"))
        {
            StartCoroutine(DesactivarElemento());
        }
    }
    private IEnumerator DesactivarElemento()
    {
        yield return new WaitForSeconds(5f);
        objeto.SetActive(false);
    }
}
