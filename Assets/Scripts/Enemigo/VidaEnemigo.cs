using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    [SerializeField] private float vidaMaxima = 100f;
    private float vidaActual;
    private Animator animator;
    private MovimientoEnemigo logicaMovimiento;

    void Start()
    {
        vidaActual = vidaMaxima;
        animator = GetComponent<Animator>();
        logicaMovimiento = GetComponent<MovimientoEnemigo>();
    }

    public void RecibirDaño(float cantidad)
    {
        vidaActual -= cantidad;
        
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
        
        // Desactivamos colisiones para que no estorbe el cadáver
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1.5f);
    }
}