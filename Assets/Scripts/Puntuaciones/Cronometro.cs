using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Si usas TextMeshPro (recomendado)

public class Cronometro : MonoBehaviour
{
    public static Cronometro instancia; // Esto permite que otros scripts lean el tiempo fácilmente
    
    public float tiempoTranscurrido = 0f;
    public bool cronometroActivo = false;
    public TextMeshProUGUI textoCronometro; 

    void Awake()
    {
       
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject); // El cronómetro sobrevive al cambio de escena
        }
        else
        {
            Destroy(gameObject); // Si ya existe uno, borramos el nuevo para no duplicar
        }
    }

    void Start()
    {
        cronometroActivo = true; // Empieza a contar en cuanto aparece
    }

void Update()
{
    if (cronometroActivo)
    {
        tiempoTranscurrido += Time.deltaTime;

        // Cada segundo, le pedimos al manager que reste la proporción
        if (PuntuacionManager.instancia != null)
        {
           
            PuntuacionManager.instancia.ModificarPuntos(-1f * Time.deltaTime);
        }
        
        ActualizarTexto();
    }
}
    void ActualizarTexto()
    {
        if (textoCronometro != null)
        {
            // Formateamos el tiempo en Minutos : Segundos
            int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60);
            int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60);
            textoCronometro.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }
}
