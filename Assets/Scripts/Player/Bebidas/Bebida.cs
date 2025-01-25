using UnityEngine;

public abstract class Bebida
{
    public abstract string Nombre { get; }
    public abstract void Activate();
    public abstract void alcoholDrink();
    public virtual string AnimTrigger => null;
}
