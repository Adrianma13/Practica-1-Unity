using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    private Slider slider;
    private Animator animator;

    private void Start()
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

    public void InicializarBarraDeVida(float vidaActual)
    {
        CambiarVidaMaxima(vidaActual);
        CambiarVidaActual(vidaActual);
    }
}
