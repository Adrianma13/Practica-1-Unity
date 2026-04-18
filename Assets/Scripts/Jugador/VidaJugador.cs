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
    private Movimiento scriptMovimiento;

    [SerializeField] private BarraDeVida barraDeVida;

    void Start()
    {
        animator = GetComponent<Animator>();
        scriptMovimiento = GetComponent<Movimiento>();

        barraDeVida = Object.FindAnyObjectByType<BarraDeVida>();

        // 1. Recuperar la vida guardada (si existe)
        float vidaPrevia = LogicaEntreEscenas.instancia.ObtenerVida();

        if (vidaPrevia == -1) // Es la primera escena o no hay datos
        {
            vidaActual = vidaMaxima;
        }
        else
        {
            vidaActual = vidaPrevia;
        }

        // 2. Inicializar la barra de UI con el valor recuperado
        if (barraDeVida != null)
        {
            barraDeVida.InicializarBarraDeVida(vidaActual, vidaMaxima);
        }
    }

    public void TomarDaño(float cantidad)
    {
        if (esInvulnerable || vidaActual <= 0) return; 

        if (TryGetComponent<Movimiento>(out Movimiento mov))
        {
            mov.ForzarDesbloqueo();
        }

        vidaActual -= cantidad;
        // Actualizar UI
        if (barraDeVida != null) barraDeVida.CambiarVidaActual(vidaActual);

        // 3. ¡IMPORTANTE! Guardar el cambio en el almacén inmediatamente
        LogicaEntreEscenas.instancia.GuardarVida(vidaActual);
        Debug.Log(gameObject.name + " recibió daño. Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir(); // Llamamos a morir y salimos
            return; 
        }

        // Solo disparamos "Hit" si seguimos vivos
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        StartCoroutine(InvulnerabilidadPostGolpe());
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
        if (scriptMovimiento != null) scriptMovimiento.enabled = false;
        
        // Desactivamos el colisionador para que los enemigos no choquen con el cuerpo
        if (TryGetComponent<Collider2D>(out Collider2D col))
        {
            col.enabled = false;
        }

        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
        
        LogicaEntreEscenas.instancia.GuardarVida(-1); // Guardamos la vida (0 o negativa) para la próxima escena
        Destroy(gameObject, 1f);

    }

    

}