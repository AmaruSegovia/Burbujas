using UnityEngine;
public class Cerveza : Bebida
{
    public override string Nombre => "Cerveza";
    public override string AnimTrigger => "BeberCerveza";
    public override void Activate()
    {
        Debug.Log("A�adir burbuja protectora aqui D:");
    }
}