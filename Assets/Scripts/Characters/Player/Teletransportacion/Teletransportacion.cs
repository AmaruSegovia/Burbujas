using System.Collections;
using UnityEngine;

public class Teletransportacion : MonoBehaviour
{

    public Transform[] puntoTeleport;
    int ultimoPunto = -1;
    private bool estaTeleportando = false;

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


        yield return new WaitForSeconds(1f);

        int posRandom;
        do
        {
            posRandom = Random.Range(0, puntoTeleport.Length);
        }
        while (posRandom == ultimoPunto); 

        transform.position = puntoTeleport[posRandom].position;
        ultimoPunto = posRandom;

        estaTeleportando = false;
    }
}