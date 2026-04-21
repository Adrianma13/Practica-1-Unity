using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDificultad : MonoBehaviour
{

    // Estas funciones las asignarás al evento "On Click ()" de cada botón
    public void SeleccionarFacil()
    {
        IniciarJuegoConDificultad(0.75f);
    }

    public void SeleccionarNormal()
    {
        IniciarJuegoConDificultad(1f);
    }

    public void SeleccionarDificil()
    {
        IniciarJuegoConDificultad(1.5f);
    }

    private void IniciarJuegoConDificultad(float multiplicador)
    {
        StartCoroutine(IniciarJuegoConDelay(multiplicador));
    }

    private IEnumerator IniciarJuegoConDelay(float multiplicador)
    {
        // Guardamos la dificultad en el Singleton
        if (LogicaEntreEscenas.instancia != null)
        {
            LogicaEntreEscenas.instancia.EstablecerDificultad(multiplicador);
        }
        else
        {
            Debug.LogError("¡Falta el objeto LogicaEntreEscenas en la escena!");
        }

        yield return new WaitForSeconds(0.2f);

        // Cargamos el primer nivel
        SceneManager.LoadScene("Pantalla Inicio");
    }
}