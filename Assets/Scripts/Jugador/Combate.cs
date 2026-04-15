using UnityEngine;
using UnityEngine.InputSystem;

public class Combate : MonoBehaviour
{
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float dañoAtaque;
    [SerializeField] private float distanciaAtaque = 1.2f;
    private bool yaAtacoEnEsteCiclo = false; // Nueva variable de control
    private Movimiento scriptMovimiento;
    private Animator animator;

    private void Start()
    {
        scriptMovimiento = GetComponent<Movimiento>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Solo actualizamos la posición si no estamos bloqueados, 
        // para que el punto de ataque no "baile" durante la animación
        if (scriptMovimiento.puedeMoverse)
        {
            ActualizarPosicionAtaque();
        }
    }

    private void ActualizarPosicionAtaque()
    {
        if (controladorAtaque != null && scriptMovimiento != null)
        {
            Vector2 nuevaPosicion = scriptMovimiento.direccionMirado * distanciaAtaque;
            controladorAtaque.localPosition = new Vector3(nuevaPosicion.x, nuevaPosicion.y, 0);
        }
    }

    // ESTA ES LA FUNCIÓN QUE DEBES VINCULAR EN EL PLAYER INPUT COMPONENT
    public void Attack(InputAction.CallbackContext context)
    {
        // Solo atacamos cuando se presiona (started o performed), no cuando se suelta
        if (context.performed)
        {
            // Si ya estamos atacando (o bloqueados), no hacer nada
            if (!scriptMovimiento.puedeMoverse) return;
            yaAtacoEnEsteCiclo = false; // Reiniciamos el control para este nuevo ataque
            IniciarAtaque();
        }
    }

    private void IniciarAtaque()
    {
        scriptMovimiento.puedeMoverse = false; // Bloqueamos movimiento
        animator.SetTrigger("Attack");         // Disparamos animación
        
        // OPCIONAL: Si no usas Animation Events, llama a Atacar() aquí.
        // Pero lo ideal es llamarlo desde la animación (ver abajo).
    }

    // Esta función se llama desde el Animation Event (el frame del golpe)
    public void Atacar()
    {
        if (yaAtacoEnEsteCiclo) return; // Evita múltiples golpes en un solo ataque
        Collider2D[] colliders = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);

        bool golpeoAlgo = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemigo"))
            {
                if(collider.TryGetComponent<VidaEnemigo>(out VidaEnemigo enemigo))
                {
                    enemigo.RecibirDaño(dañoAtaque);
                    golpeoAlgo = true;
                }
            }
        }
        if (golpeoAlgo)
        {
            yaAtacoEnEsteCiclo = true; // Marcamos que ya hemos golpeado en este ciclo de ataque
        }
    }

    // Esta función se llama desde el Animation Event (el último frame)
    public void FinalizarAtaque()
    {
        scriptMovimiento.puedeMoverse = true; 
    }

    private void OnDrawGizmosSelected()
    {
        if (controladorAtaque == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }
}