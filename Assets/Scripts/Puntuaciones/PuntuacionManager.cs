using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuntuacionManager : MonoBehaviour
{
    public static PuntuacionManager instancia;

    public float puntosActuales = 100f;
    public TextMeshProUGUI textoPuntos;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Función universal para añadir o quitar puntos
    public void ModificarPuntos(float cantidad)
    {
        puntosActuales += cantidad;
        if (puntosActuales < 0) puntosActuales = 0; // No queremos puntos negativos
        ActualizarUI();
    }

    void ActualizarUI()
    {
        if (textoPuntos != null)
        {
            textoPuntos.text = "Puntos: " + Mathf.FloorToInt(puntosActuales).ToString();
        }
    }
}