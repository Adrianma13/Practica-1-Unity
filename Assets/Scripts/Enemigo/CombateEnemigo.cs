using UnityEngine;

public class CombateEnemigo : MonoBehaviour
{
    [Header("Configuración de Ataque")]
    [SerializeField] private float daño = 10f;
    [SerializeField] private float radioAtaque = 1f;
    [SerializeField] private float tiempoEntreAtaques = 1.5f;
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private LayerMask capaJugador;

    private float cronometroAtaque;
    private Animator animator;
    private LogicaEnemigo logicaMovimiento;

    void Start()
    {
        animator = GetComponent<Animator>();
        logicaMovimiento = GetComponent<LogicaEnemigo>();
    }

    void Update()
    {
        // El cronómetro siempre baja
        if (cronometroAtaque > 0)
        {
            cronometroAtaque -= Time.deltaTime;
        }

        // Detectamos si el jugador está en rango para atacar
        float distancia = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        
        // Si el jugador está cerca y el enemigo puede atacar
        if (distancia <= logicaMovimiento.GetDistanciaAtaque() && cronometroAtaque <= 0)
        {
            Atacar();
        }
    }

    private void Atacar()
    {
        cronometroAtaque = tiempoEntreAtaques;
        
        // Activamos la animación de ataque del enemigo
        animator.SetTrigger("Attack");

        // El daño se hace aquí (o mejor aún, mediante un Animation Event)
        EjecutarDaño();
    }


    public void EjecutarDaño()
{
    // Buscamos al jugador en el radio de ataque
    Collider2D objetoGolpeado = Physics2D.OverlapCircle(controladorAtaque.position, radioAtaque, capaJugador);

    if (objetoGolpeado != null)
    {
        // Intentamos obtener el componente de vida del jugador
        if (objetoGolpeado.TryGetComponent<VidaJugador>(out VidaJugador vida))
        {
            vida.TomarDaño(daño);
        }
    }
}

    private void OnDrawGizmosSelected()
    {
        if (controladorAtaque != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
        }
    }
}