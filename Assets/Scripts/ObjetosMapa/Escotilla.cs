using UnityEngine;
using UnityEngine.SceneManagement; // ¡IMPORTANTE! Necesario para cambiar de escena

public class Escotilla : MonoBehaviour
{
    public bool estaAbierta = false;
    public string llaveNecesaria = "LlaveEscotilla"; 
    public string nombreEscenaDestino;

    public Sprite spriteCerrada;
    public Sprite spriteAbierta;
    private SpriteRenderer sr;
    private Collider2D col;
    public AudioSource audioEscotillaCerrada;
    public AudioSource audioEscotillaAbierta;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        ActualizarVisual();
    }

    public void IntentarAbrir(PlayerInteraction2D jugador)
    {
        if (estaAbierta)
        {
            ViajarALaSiguienteEscena();
        }
      
        else if (jugador.TieneObjeto(llaveNecesaria))
        {
            estaAbierta = true;
            ActualizarVisual();
            audioEscotillaAbierta.Play();
            Debug.Log("Escotilla abierta con " + llaveNecesaria);
        }
        else
        {
            audioEscotillaCerrada.Play();
            Debug.Log("Necesitas la " + llaveNecesaria);
        }
    }

    void ViajarALaSiguienteEscena()
    {
        if (!string.IsNullOrEmpty(nombreEscenaDestino))
        {
            SceneManager.LoadScene(nombreEscenaDestino);
        }
        else
        {
            Debug.LogError("¡No has puesto el nombre de la escena en el Inspector!");
        }
    }

    void ActualizarVisual()
    {
        sr.sprite = estaAbierta ? spriteAbierta : spriteCerrada;
        // Mantenemos el collider como trigger para que el jugador sepa que puede interactuar
        col.isTrigger = true;
    }
}