
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ControladorDatosJuego : MonoBehaviour
{
    public GameObject jugador;
    public string archivoDeGuardado;
    public DatosJuego datosJuego = new DatosJuego();


    public void Awake()
    {
        archivoDeGuardado = Application.dataPath + "/datosJuego.json";

        jugador = GameObject.FindGameObjectWithTag("Player");

        //CargarDatos();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            CargarDatos();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GuardarDatos();
        }


    }

    private void CargarDatos() {
        if (File.Exists(archivoDeGuardado))
        {
            string contenido = File.ReadAllText(archivoDeGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuego>(contenido);

            Debug.Log("Posicion de jugador: "+ datosJuego.posicion);
            jugador.transform.position = datosJuego.posicion;
            //jugador.GetComponent<VidaJudador>.cantidadVida = datosJuego.vida;
        }
        else
        {

            Debug.Log("El archivo no existe");
        }
    }
    private void GuardarDatos() {
        DatosJuego nuevosDatos = new DatosJuego()
        {

            posicion = jugador.transform.position
            //vida = jugador.GetComponent<VidaJudador>.cantidadVida

        };
       
        string cadenaJSON = JsonUtility.ToJson(nuevosDatos);
        File.WriteAllText(archivoDeGuardado, cadenaJSON);

        Debug.Log("Archivo Guardado");

    }

}
