using UnityEngine;

public class ControlPause : MonoBehaviour
{
    // Esta variable estática persistirá entre escenas
    public static ControlPause instancia;

    void Awake()
    {
        // Verificamos si ya existe una instancia de este menú
        if (instancia == null)
        {
            // Si es el primero, lo guardamos y le decimos que no se destruya
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existe uno (el que venía de la escena anterior), 
            // destruimos el nuevo que acaba de aparecer en esta escena.
            Debug.Log("Menú duplicado detectado, destruyendo...");
            Destroy(gameObject);
        }
    }
}