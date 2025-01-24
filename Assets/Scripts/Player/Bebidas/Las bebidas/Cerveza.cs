using UnityEngine;
public class Cerveza : Bebida
{
    public override string Nombre => "Cerveza";
    public override string AnimTrigger => "BeberCerveza";
    public override void Activate()
    {
        Debug.Log("Añadir burbuja protectora aqui D:");
    }
}