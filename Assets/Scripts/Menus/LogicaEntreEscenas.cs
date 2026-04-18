using UnityEngine;

public class LogicaEntreEscenas : MonoBehaviour
{
    public static LogicaEntreEscenas instancia;

    // Guardamos la vida aquí. La inicializamos en -1 para saber si es la primera vez que jugamos.
    private float vidaGuardada = -1; 

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

    public void GuardarVida(float vida)
    {
        vidaGuardada = vida;
    }

    public float ObtenerVida()
    {
        return vidaGuardada;
    }
}