using UnityEngine;
public class Cerveza : Bebida
{
    public override string Nombre => "Cerveza";

    public override void Activate()
    {
        Debug.Log("Añadir burbuja protectora aqui D:");
    }
}