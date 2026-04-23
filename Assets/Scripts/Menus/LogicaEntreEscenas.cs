using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaEntreEscenas : MonoBehaviour
{
    public static LogicaEntreEscenas instancia;

    // Guardamos la vida aquí. La inicializamos en -1 para saber si es la primera vez que jugamos.
    private float vidaGuardada = -1;
    public int idArmadura1Guardada = 0; // 0 = no tiene, 1 = tiene

    public float multiplicadorDificultad = 1f;


    private void Awake()
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

    public void ValoresPorDefecto()
    {

       if(SceneManager.GetActiveScene().name == "ConfigNivel")
        {
            vidaGuardada = -1; // Reiniciamos la vida guardada al volver al menú principal
            idArmadura1Guardada = 0; // Reiniciamos el estado del armadura al volver al menú principal
            multiplicadorDificultad = 1f; // Reiniciamos la dificultad al volver al menú principal
        }

    }
    public void GuardarVida(float vida)
    {
        vidaGuardada = vida;
    }

    public float ObtenerVida()
    {
        return vidaGuardada;
    }

    public void EstablecerDificultad(float multiplicador)
    {
        ValoresPorDefecto(); // Reiniciamos los valores por defecto cada vez que se selecciona una dificultad
        multiplicadorDificultad = multiplicador;
    }
}