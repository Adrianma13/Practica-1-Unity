using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLlave : MonoBehaviour
{
    public string nombreDeEstaLlave = "LlaveEscotilla"; // Configúralo en el Inspector
    public AudioSource audioSource; // Referencia al AudioSource para reproducir el sonido al recoger la llave


    public void Interactuar(PlayerInteraction2D jugador)
    {
        // Usamos el nuevo sistema de lista
       AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
        jugador.RecogerObjeto(nombreDeEstaLlave);

        Debug.Log("Has recogido: " + nombreDeEstaLlave);
        Destroy(gameObject); // Destruye la llave después de 1 segundo para que el sonido se reproduzca completo
    }
}