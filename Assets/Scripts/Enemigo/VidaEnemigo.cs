using UnityEngine;
using System.Collections;

public class VidaEnemigo : MonoBehaviour
{
    [Header("Estadísticas")]
    [SerializeField] private float vidaMaxima = 100f;
    private float vidaActual;

    private Animator animator;
    private Rigidbody2D rb;
    private LogicaEnemigo logicaMovimiento;

    void Start()
    {
        vidaActual = vidaMaxima;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        logicaMovimiento = GetComponent<LogicaEnemigo>();
    }

    public void RecibirDaño(float daño)
    {
        vidaActual -= daño;
        Debug.Log(gameObject.name + " recibió daño. Vida restante: " + vidaActual);

        if(TryGetComponent<LogicaEnemigo>(out LogicaEnemigo logica))
        {
            logica.puedeMoverse = true;
        }
        // 1. Activar animación de Hit
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        // 2. Comprobar si ha muerto
        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        // Desactivamos la lógica para que no siga atacando mientras muere
        if (logicaMovimiento != null) logicaMovimiento.enabled = false;
        
        // Si tienes animación de muerte:
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Se llama desde un Animation Event al final de la animación de muerte
    public void EventoMuerteFinal()
    {
        Destroy(gameObject);
    }
}