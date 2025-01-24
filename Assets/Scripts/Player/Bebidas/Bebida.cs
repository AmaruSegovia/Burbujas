using UnityEngine;

public abstract class Bebida
{
    public abstract string Nombre { get; }
    public abstract void Activate();
    public virtual string AnimTrigger => null;
}
