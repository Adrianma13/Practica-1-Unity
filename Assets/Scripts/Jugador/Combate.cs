using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combate : MonoBehaviour
{
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float dañoAtaque;
    [SerializeField] private float distanciaAtaque = 1.2f; // Qué tan lejos del cuerpo sale el hit

    private Movimiento scriptMovimiento;

    private void Start()
    {
        scriptMovimiento = GetComponent<Movimiento>();
    }

    private void Update()
    {
        // Actualizamos la posición del punto de ataque constantemente
        ActualizarPosicionAtaque();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Atacar();
        }
    }

    private void ActualizarPosicionAtaque()
    {
        if (controladorAtaque != null && scriptMovimiento != null)
        {
            // Calculamos la nueva posición relativa al centro del jugador
            Vector2 nuevaPosicion = scriptMovimiento.direccionMirado * distanciaAtaque;
            controladorAtaque.localPosition = new Vector3(nuevaPosicion.x, nuevaPosicion.y, 0);
        }
    }

    private void Atacar()
    {
        // Usamos OverlapCircleAll para detectar enemigos
        Collider2D[] colliders = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemigo"))
            {
                // Un pequeño consejo: usa TryGetComponent para evitar errores si falta el script
                if(collider.TryGetComponent<VidaEnemigo>(out VidaEnemigo enemigo))
                {
                    enemigo.RecibirDaño(dañoAtaque);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (controladorAtaque == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }

}
