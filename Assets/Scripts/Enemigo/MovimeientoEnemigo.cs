using UnityEngine;
using UnityEngine.AI;

using UnityEngine;
using UnityEngine.AI;

public class MovimientoEnemigo : MonoBehaviour
{
    public enum EstadoIA { Patrullando, Persiguiendo, Atacando }
    
    [Header("Estado Actual")]
    public EstadoIA estadoActual = EstadoIA.Patrullando;

    [Header("Configuración IA")]
    [SerializeField] private float radioDeteccion = 7f;
    [SerializeField] private float radioAtaque = 1.5f;
    [SerializeField] private float tiempoEsperaEnPunto = 2f;
    [SerializeField] private Transform[] puntosPatrulla;

    [HideInInspector] public bool puedeMoverse = true; 

    private NavMeshAgent agente;
    private Transform jugador;
    private Animator animator;
    private int indicePatrullaActual = 0;
    private float cronometroEspera;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        jugador = GameObject.FindGameObjectWithTag("Player").transform;

        agente.updateRotation = false;
        agente.updateUpAxis = false;
        
        if (puntosPatrulla.Length > 0)
            agente.SetDestination(puntosPatrulla[indicePatrullaActual].position);
    }

    void Update()
    {
        if (jugador == null || !puedeMoverse) {
            DetenerEnemigo();
            return;
        }

        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        switch (estadoActual)
        {
            case EstadoIA.Patrullando:
                LógicaPatrulla();
                if (distanciaAlJugador < radioDeteccion) estadoActual = EstadoIA.Persiguiendo;
                break;

            case EstadoIA.Persiguiendo:
                LógicaPersecucion();
                if (distanciaAlJugador <= radioAtaque) estadoActual = EstadoIA.Atacando;
                if (distanciaAlJugador > radioDeteccion) estadoActual = EstadoIA.Patrullando;
                break;

            case EstadoIA.Atacando:
                LógicaAtaque();
                if (distanciaAlJugador > radioAtaque) estadoActual = EstadoIA.Persiguiendo;
                break;
        }
        ActualizarAnimaciones(agente.velocity.normalized);
    }

    void LógicaPatrulla() {
        agente.isStopped = false;
        if (!agente.pathPending && agente.remainingDistance < 0.5f) {
            cronometroEspera += Time.deltaTime;
            if (cronometroEspera >= tiempoEsperaEnPunto) {
                indicePatrullaActual = (indicePatrullaActual + 1) % puntosPatrulla.Length;
                agente.SetDestination(puntosPatrulla[indicePatrullaActual].position);
                cronometroEspera = 0;
            }
        }
    }

    void LógicaPersecucion() {
        agente.isStopped = false;
        agente.SetDestination(jugador.position);
    }

    void LógicaAtaque() {
        agente.isStopped = true;
        // Llama a cualquier script de combate que tenga el enemigo
        SendMessage("IntentarAtacar", SendMessageOptions.DontRequireReceiver);
    }

    void DetenerEnemigo() {
        agente.isStopped = true;
        ActualizarAnimaciones(Vector2.zero);
    }

    void ActualizarAnimaciones(Vector2 dir) {
        bool caminando = dir.magnitude > 0.1f;
        animator.SetBool("isWalking", caminando);
        if (caminando) {
            animator.SetFloat("InputX", dir.x);
            animator.SetFloat("InputY", dir.y);
            animator.SetFloat("LastInputX", dir.x);
            animator.SetFloat("LastInputY", dir.y);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioAtaque);
    }
}