using System.Collections;
using UnityEngine;

public class Teletransportacion : MonoBehaviour
{
    public Transform[] puntoTeleport;
    int ultimoPunto = -1;
    private bool estaTeleportando = false;
    public FadeOut fadeOut;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && !estaTeleportando)
        {
            StartCoroutine(RespawnTeleport());
        }
    }

    private IEnumerator RespawnTeleport()
    {
        estaTeleportando = true;

        yield return StartCoroutine(fadeOut.DarkenSprite());

        int posRandom;
        do
        {
            posRandom = Random.Range(0, puntoTeleport.Length);
        }
        while (posRandom == ultimoPunto);

        transform.position = puntoTeleport[posRandom].position;
        ultimoPunto = posRandom;

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(fadeOut.FadeOutSprite());

        estaTeleportando = false;
    }
}