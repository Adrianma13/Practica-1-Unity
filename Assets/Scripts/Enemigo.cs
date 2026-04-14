using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Estadísticas")]
    [SerializeField] private float vidaMaxima = 100f;
    private float vidaActual;

    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        vidaActual = vidaMaxima;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void RecibirDaño(float cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Enemigo recibió daño: " + cantidad + ", Vida restante: " + vidaActual);

        // Lanzamos el Trigger del Animator
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        animator.SetTrigger("Muerte");
        Destroy(gameObject);
        Debug.Log("Enemigo ha muerto.");
    }
}