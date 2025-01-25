using System.Collections;
using UnityEngine;

public class Cola : Bebida
{
    public override string Nombre => "Cola";
    public override string AnimTrigger => "BeberCola";
    public override void Activate()
    {
        Debug.Log("Tomaste coca cola, dile chau a tus ri�ones");
    }
    public override void alcoholDrink(){
        AlcoholBar.Instance.beber(1);
    }
}