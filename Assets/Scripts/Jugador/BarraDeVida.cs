using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarraDeVida : MonoBehaviour
{
    private Slider slider;
    private Animator animator;
    [SerializeField] private TextMeshProUGUI textoVida; // Referencia al texto que muestra la vida actual
    private float vidaMaxima;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        animator = GetComponent<Animator>();
    }

    public void CambiarVidaMaxima(float vidaMaxima)
    {
        slider.maxValue = vidaMaxima;
    }

    public void CambiarVidaActual(float vidaActual)
    {
        slider.value = vidaActual;
        animator.SetTrigger("Golpe");
        MostrarTextoVida(vidaActual, vidaMaxima);
    }

    public void InicializarBarraDeVida(float vidaActual, float vidaMaxima)
    {
        CambiarVidaMaxima(vidaMaxima);
        CambiarVidaActual(vidaActual);
        MostrarTextoVida(vidaActual, vidaMaxima);
        this.vidaMaxima = vidaMaxima; // Guardamos la vida máxima para mostrarla en el texto
    }

    public void MostrarTextoVida(float vidaActual, float vidaMaxima)
    {
        if (textoVida != null)
        {
            textoVida.text = $"{vidaActual}/{vidaMaxima}";
        }
    }
}
