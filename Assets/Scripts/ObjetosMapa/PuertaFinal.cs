using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class PuertaFinal : MonoBehaviour
{
    [Header("Configuración")]
    public string nombreEscenaMenu;// El nombre de tu escena de inicio

    public void TerminarPartida()
    {
        Debug.Log("Partida terminada. Volviendo al menú...");
        SceneManager.LoadScene(nombreEscenaMenu);
    }
}