using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Esta interfaz se utiliza para manejar colisiones entre un objeto que cause daño  y el jugador.*/
public interface IDamageHandler
{
    /*Este metodo que debe implementarse en las clases que utilicen esta interfaz.
       Se ejecuta cuando ocurre una colision entre el jugador y otro objeto del mapa (por ej: enemigos u obstaculos).*/
    void TakeDamage(Player player);
}