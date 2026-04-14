using UnityEngine;

public class LogicaEnemigo : MonoBehaviour
{
    [Header("Configuración Movimiento")]
    [SerializeField] private float velocidad = 3f;
    [SerializeField] private float distanciaDeteccion = 10f;
    [SerializeField] private float distanciaAtaque = 1.5f;

    private Transform jugador;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 direccionMovimiento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // Buscamos al jugador por su Tag
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (jugador == null) return;

        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        // Si está cerca pero no lo suficiente para atacar, lo sigue
        if (distanciaAlJugador < distanciaDeteccion && distanciaAlJugador > distanciaAtaque)
        {
            direccionMovimiento = (jugador.position - transform.position).normalized;
            ActualizarAnimaciones(true);
        }
        else
        {
            direccionMovimiento = Vector2.zero;
            ActualizarAnimaciones(false);
        }
    }

    void FixedUpdate()
    {
        // Movimiento físico
        rb.MovePosition(rb.position + direccionMovimiento * velocidad * Time.fixedDeltaTime);
    }

    void ActualizarAnimaciones(bool caminando)
    {
        animator.SetBool("isWalking", caminando);

        if (caminando)
        {
            // Pasamos los ejes X e Y al Blend Tree para las 8 direcciones
            animator.SetFloat("InputX", direccionMovimiento.x);
            animator.SetFloat("InputY", direccionMovimiento.y);
            
            // También guardamos la última dirección para el Idle
            animator.SetFloat("LastInputX", direccionMovimiento.x);
            animator.SetFloat("LastInputY", direccionMovimiento.y);
        }
    }

    public float GetDistanciaAtaque() {
    return distanciaAtaque;
}
}