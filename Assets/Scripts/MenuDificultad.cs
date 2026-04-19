using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDificultad : MonoBehaviour
{
    [SerializeField] private string nombrePrimeraEscena = "Nivel1"; // Pon el nombre de tu escena aquí

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
        // Guardamos la dificultad en el Singleton
        if (LogicaEntreEscenas.instancia != null)
        {
            LogicaEntreEscenas.instancia.EstablecerDificultad(multiplicador);
        }
        else
        {
            Debug.LogError("¡Falta el objeto LogicaEntreEscenas en la escena!");
        }

        // Cargamos el primer nivel
        SceneManager.LoadScene("Pantalla Inicio");
    }
}