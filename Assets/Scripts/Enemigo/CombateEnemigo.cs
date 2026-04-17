using UnityEngine;

public class CombateEnemigo : MonoBehaviour
{
    [Header("Configuración Base")]
    [SerializeField] protected float daño = 10f;
    [SerializeField] protected float tiempoEntreAtaques = 1.5f;
    [SerializeField] protected LayerMask capaJugador;
    
    [Header("Ataque Circular / Direccional")]
    [SerializeField] protected Transform controladorAtaque;
    [SerializeField] protected float radioAtaque = 1f; // Qué tan grande es el golpe
    [SerializeField] protected float distanciaDelCuerpo = 1f; // Qué tan lejos del centro aparece el golpe

    protected float cronometroAtaque;
    protected Animator animator;
    protected MovimientoEnemigo logicaMovimiento;
    protected Transform jugador;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        logicaMovimiento = GetComponent<MovimientoEnemigo>();
        // Buscamos al jugador una sola vez para ahorrar rendimiento
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) jugador = playerObj.transform;
    }

    protected virtual void Update()
    {
        if (cronometroAtaque > 0) cronometroAtaque -= Time.deltaTime;

        // Actualizamos la posición del controlador para que siempre apunte al jugador
        ActualizarPosicionAtaque();
    }

    protected void ActualizarPosicionAtaque()
    {
        if (controladorAtaque != null && jugador != null && logicaMovimiento.puedeMoverse)
        {
            // Calculamos la dirección hacia el jugador
            Vector2 direccion = (jugador.position - transform.position).normalized;
            
            // Colocamos el controlador en esa dirección multiplicado por la distancia
            controladorAtaque.localPosition = new Vector3(direccion.x * distanciaDelCuerpo, direccion.y * distanciaDelCuerpo, 0);
        }
    }

    public void IntentarAtacar()
    {
        if (cronometroAtaque <= 0 && logicaMovimiento.puedeMoverse)
        {
            cronometroAtaque = tiempoEntreAtaques;
            logicaMovimiento.puedeMoverse = false; 
            animator.SetTrigger("Attack");
        }
    }

    // Se llama desde el Animation Event: "EjecutarDaño"
    public virtual void EjecutarDaño() 
    {
        Collider2D hit = Physics2D.OverlapCircle(controladorAtaque.position, radioAtaque, capaJugador);
        if (hit != null && hit.TryGetComponent<VidaJugador>(out VidaJugador vida))
            vida.TomarDaño(daño);
    }

    public void FinalizarAtaqueEnemigo()
    {
        logicaMovimiento.puedeMoverse = true;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        if (controladorAtaque != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
        }
    }
}