using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class PuertaFinal : MonoBehaviour
{
    [Header("Configuración")]
    public string nombreEscenaMenu;

    // Dentro de PuertaFinal.cs
    public void TerminarPartida()
    {
        if (Cronometro.instancia != null) Cronometro.instancia.cronometroActivo = false;

        SceneManager.LoadScene("PantallaNombre");
    }
}