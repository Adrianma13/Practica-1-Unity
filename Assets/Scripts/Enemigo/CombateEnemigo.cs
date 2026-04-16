using UnityEngine;

public class CombateEnemigo : MonoBehaviour
{
    [Header("Configuración Base")]
    [SerializeField] protected float daño = 10f;
    [SerializeField] protected float tiempoEntreAtaques = 1.5f;
    [SerializeField] protected LayerMask capaJugador;
    
    [Header("Ataque Circular (Normal)")]
    [SerializeField] protected Transform controladorAtaque;
    [SerializeField] protected float radioAtaque = 1f;

    protected float cronometroAtaque;
    protected Animator animator;
    protected MovimientoEnemigo logicaMovimiento;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        logicaMovimiento = GetComponent<MovimientoEnemigo>();
    }

    protected virtual void Update()
    {
        if (cronometroAtaque > 0) cronometroAtaque -= Time.deltaTime;
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

    public virtual void EjecutarDaño() // Para enemigos normales
    {
        Collider2D hit = Physics2D.OverlapCircle(controladorAtaque.position, radioAtaque, capaJugador);
        if (hit != null && hit.TryGetComponent<VidaJugador>(out VidaJugador vida))
            vida.TomarDaño(daño);
    }

    public void FinalizarAtaqueEnemigo()
    {
        logicaMovimiento.puedeMoverse = true;
    }
}