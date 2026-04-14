using UnityEngine;
using System.Collections;

public class VidaJugador : MonoBehaviour
{
    [Header("Estadísticas")]
    [SerializeField] private float vidaMaxima = 100f;
    private float vidaActual;

    private Animator animator;
    private bool esInvulnerable = false;
    [SerializeField] private float tiempoInvulnerabilidad = 0.5f;

    void Start()
    {
        vidaActual = vidaMaxima;
        animator = GetComponent<Animator>();
    }

    public void TomarDaño(float cantidad)
    {
        if (esInvulnerable) return; // Evita que un solo ataque golpee múltiples veces

        vidaActual -= cantidad;
        Debug.Log("Vida del Jugador: " + vidaActual);

        // Disparamos la animación de Hit del jugador
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        if (vidaActual <= 0)
        {
            Morir();
        }
        else
        {
            // Pequeño tiempo de gracia para no morir instantáneamente
            StartCoroutine(InvulnerabilidadPostGolpe());
        }
    }

    private IEnumerator InvulnerabilidadPostGolpe()
    {
        esInvulnerable = true;
        // Opcional: podrías hacer que el sprite parpadee aquí
        yield return new WaitForSeconds(tiempoInvulnerabilidad);
        esInvulnerable = false;
    }

    private void Morir()
    {
        Debug.Log("El jugador ha muerto");
        // Aquí podrías recargar la escena o mostrar pantalla de Game Over
        animator.SetTrigger("Muerte");
        Destroy(gameObject, 1f); // Destruye el objeto después de 1 segundo para que se vea la animación
    }
}