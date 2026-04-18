using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    private Slider slider;
    private Animator animator;

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
    }

    public void InicializarBarraDeVida(float vidaActual, float vidaMaxima)
    {
        CambiarVidaMaxima(vidaMaxima);
        CambiarVidaActual(vidaActual);
    }
}
