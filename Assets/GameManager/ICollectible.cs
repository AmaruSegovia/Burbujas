using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Esta interfaz se utiliza para manejar colisiones entre un objeto como items en el mapa */
public interface ICollectible
{
    /*Este metodo que debe implementarse en las clases que utilicen esta interfaz.
      Se ejecuta cuando ocurre una colisión entre el jugador y otro objeto del mapa (por ej: coins u power up).*/
void Collect(Player player);
}
