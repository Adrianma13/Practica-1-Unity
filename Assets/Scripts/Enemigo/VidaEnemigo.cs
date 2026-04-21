using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaEnemigo : MonoBehaviour
{
    [SerializeField] private float vidaMaxima = 100f;
    private float vidaActual;
    private Animator animator;
    private MovimientoEnemigo logicaMovimiento;
    
    public TrapTrigger trapTrigger; // Referencia al script de la trampa

    [Header("Efecto Visual de Daño")]
    [SerializeField] private Color colorDaño = Color.red; // Color al recibir el golpe
    private SpriteRenderer spriteRenderer;
    private Color colorOriginal;

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

        // Guardamos la referencia a la imagen y su color original (normalmente blanco)
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            colorOriginal = spriteRenderer.color;
        }
        
    }

    public void RecibirDaño(float cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log($"Enemigo recibió {cantidad} de daño. Vida actual: {vidaActual}/{vidaMaxima}");
        // Desbloqueamos el movimiento por si el golpe interrumpió un ataque
        if (logicaMovimiento != null) logicaMovimiento.puedeMoverse = true;

        if (spriteRenderer != null)
        {
            StartCoroutine(EfectoParpadeo());
        }

        if (vidaActual <= 0)
        {
            Morir();
        }
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

    // Esta corrutina se ejecuta en segundo plano sin detener el juego
    private IEnumerator EfectoParpadeo()
    {
        // Cambiamos al color de daño
        spriteRenderer.color = colorDaño;
        
        // Esperamos 0.1 segundos (puedes ajustar este valor)
        yield return new WaitForSeconds(0.2f);
        
        // Volvemos al color normal
        spriteRenderer.color = colorOriginal;
    }
}