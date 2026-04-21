using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject muro; // Arrastra aquí el muro desactivado
    public bool seActivaSoloUnaVez = true;
    private bool yaActivado = false;
    public GameObject muroFinal;

    // Esta función se ejecuta automáticamente cuando algo entra en el Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprobamos si lo que entró es el jugador
        if (collision.CompareTag("Player") && !yaActivado)
        {
            ActivarMuro();
        }
    }

    void ActivarMuro()
    {
        if (muro != null)
        {
            muro.SetActive(true); // ¡El muro aparece!
            Debug.Log("¡Trampa activada! El camino está bloqueado.");
            muroFinal.SetActive(true);
            if (seActivaSoloUnaVez)
            {
                yaActivado = true;
            }
        }
    }
     public void DesactivarMuro()
    {
        if (muro != null)
        {
            muroFinal.SetActive(false); // El muro desaparece
            Debug.Log("El muro ha sido desactivado.");
        }
    }
    
}