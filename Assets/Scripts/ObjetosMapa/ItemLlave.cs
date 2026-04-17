using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLlave : MonoBehaviour
{
    public string nombreDeEstaLlave = "LlaveEscotilla"; // Configúralo en el Inspector

    public void Interactuar(PlayerInteraction2D jugador)
    {
        // Usamos el nuevo sistema de lista
        jugador.RecogerObjeto(nombreDeEstaLlave); 
        Debug.Log("Has recogido: " + nombreDeEstaLlave);
        Destroy(gameObject);
    }
}