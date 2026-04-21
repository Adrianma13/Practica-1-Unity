using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaEnemigo : MonoBehaviour
{
    [SerializeField] private float vidaMaxima = 100f;
    private float vidaActual;
    private Animator animator;
    private MovimientoEnemigo logicaMovimiento;
    
    public TrapTrigger trapTrigger; // Referencia al script de la trampa

    void Start()
    {
        // Obtenemos el multiplicador (si no existe el singleton, usamos 1 por seguridad)
        float multiplicador = LogicaEntreEscenas.instancia != null ? LogicaEntreEscenas.instancia.multiplicadorDificultad : 1f;

        // Aplicamos la dificultad a la vida máxima
        vidaMaxima = vidaMaxima * multiplicador;
        vidaActual = vidaMaxima;

        //vidaActual = vidaMaxima;
        animator = GetComponent<Animator>();
        logicaMovimiento = GetComponent<MovimientoEnemigo>();
        
    }

    public void RecibirDaño(float cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log($"Enemigo recibió {cantidad} de daño. Vida actual: {vidaActual}/{vidaMaxima}");
        // Desbloqueamos el movimiento por si el golpe interrumpió un ataque
        if (logicaMovimiento != null) logicaMovimiento.puedeMoverse = true;

        if (vidaActual <= 0) Morir();
        else if (animator != null) animator.SetTrigger("Hit");
    }

    private void Morir()
    {
        PuntuacionManager.instancia.ModificarPuntos(50f); // Recompensa por matar al enemigo
        if (logicaMovimiento != null) logicaMovimiento.enabled = false;
        if (animator != null) animator.SetTrigger("Death");
        if (TryGetComponent<Collider2D>(out Collider2D col))
        {
            col.enabled = false;
        }
        
        // Desactivamos colisiones para que no estorbe el cadáver
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1f);
        if(SceneManager.GetActiveScene().name == "Pantalla Jefe")
            trapTrigger.DesactivarMuro(); // Llamamos a la función para desactivar el muro al morir el enemigo
      
    }
}