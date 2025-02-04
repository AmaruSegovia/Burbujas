using UnityEngine;

public abstract class BebidaSO : ScriptableObject
{
    public string Nombre;
    public Sprite Icono;
    public string AnimTrigger;
    public int AlcoholValue;
    public AudioClip sonido;
    public float cooldown = 3f;

    public abstract void Activate(Transform jugador);
}
