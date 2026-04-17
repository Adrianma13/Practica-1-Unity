using UnityEngine;

public class Cofre : MonoBehaviour
{
    public bool estaAbierto = false;
    public string objetoContenido; // Escribe aquí "LlaveRoja", "Espada", etc.
    
    public Sprite spriteCerrado;
    public Sprite spriteAbierto;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ActualizarVisual();
    }

    public void AbrirCofre(PlayerInteraction2D jugador)
    {
        if (!estaAbierto)
        {
            estaAbierto = true;
            
            // Le damos al jugador el string que hayamos escrito en el Inspector
            jugador.RecogerObjeto(objetoContenido);
            
            ActualizarVisual();
        }
    }

    void ActualizarVisual()
    {
        sr.sprite = estaAbierto ? spriteAbierto : spriteCerrado;
    }
}