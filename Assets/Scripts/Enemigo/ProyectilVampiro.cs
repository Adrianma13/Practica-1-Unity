using UnityEngine;

public class ProyectilJefe : MonoBehaviour
{
    public float velocidad = 8f;
    public float daño = 15f;
    public float tiempoVida = 3f;

    void Start()
    {
        Destroy(gameObject, tiempoVida); // Se destruye solo tras X segundos
    }

    void Update()
    {
        // Se mueve siempre hacia adelante (derecha del objeto)
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<VidaJugador>().TomarDaño(daño);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Pared"))
        {
            Destroy(gameObject);
        }
    }
}