using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction2D : MonoBehaviour
{
    public float radioInteraccion = 1.5f; // Distancia del círculo de detección
    public LayerMask capaInteractuable;   // Para filtrar qué objetos buscar
    public Transform puntoInteraccion;    // Un objeto vacío frente al personaje

    public List<string> inventario = new List<string>(); // Para guardar los objetos que el jugador ha recogido


    [Header("Configuración de Armadura")]
    public RuntimeAnimatorController controladorConArmadura; // El nuevo Animator

    private Animator animator;

    void Awake()
    {
        // Obtenemos el componente Animator del jugador
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        Collider2D objetoTocado = Physics2D.OverlapCircle(puntoInteraccion.position, radioInteraccion, capaInteractuable);

        if (objetoTocado != null)
        {
            // 1. Puertas
            Puerta2D puerta = objetoTocado.GetComponentInParent<Puerta2D>();
            if (puerta != null) puerta.Interactuar();

            // 2. Llaves
            ItemLlave llave = objetoTocado.GetComponent<ItemLlave>();
            if (llave != null) llave.Interactuar(this);

            // 3. Escotillas
            Escotilla escotilla = objetoTocado.GetComponent<Escotilla>();
            if (escotilla != null) escotilla.IntentarAbrir(this);

            // 4. NUEVO: Cofres
            Cofre cofre = objetoTocado.GetComponent<Cofre>();
            if (cofre != null) cofre.AbrirCofre(this);
            
            PuertaFinal final = objetoTocado.GetComponent<PuertaFinal>();
            if (final != null)
            {
                final.TerminarPartida();
            } else
            {
                Debug.Log("No hay nada con lo que interactuar aquí.");
            }
        }
    }

    // Esto es para que puedas ver el círculo en el Editor de Unity
    private void OnDrawGizmosSelected()
    {
        if (puntoInteraccion != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(puntoInteraccion.position, radioInteraccion);
        }
    }

    public void RecogerObjeto(string nombreObjeto)
    {
        inventario.Add(nombreObjeto);
        Debug.Log("Inventario: Has obtenido " + nombreObjeto);
        if (nombreObjeto == "Armadura")
        {
            EquiparArmadura();
        }
    }
    void EquiparArmadura()
    {
        if (controladorConArmadura != null && animator != null)
        {
            // Cambiamos el controlador completo por el nuevo
            animator.runtimeAnimatorController = controladorConArmadura;
            Debug.Log("¡Animator cambiado! Ahora tienes movimientos de armadura.");
        }
    }
    // Función para consultar si tenemos algo (útil para puertas con llave)
    public bool TieneObjeto(string nombreObjeto)
    {
        return inventario.Contains(nombreObjeto);
    }
}