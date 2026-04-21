using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta2D : MonoBehaviour
{
    public bool estaAbierta = false;
    
    [Header("Referencias de las Hojas")]
    public SpriteRenderer hojaIzquierda;
    public SpriteRenderer hojaDerecha;

    [Header("Sprites")]
    public Sprite spriteCerradaIzq;
    public Sprite spriteAbiertaIzq;
    public Sprite spriteCerradaDer;
    public Sprite spriteAbiertaDer;
    public AudioSource audioSource ;

    void Start()
    {
        // Al empezar, nos aseguramos de que los sprites coincidan con el estado
        ActualizarEstado();
    }

    public void Interactuar()
    {
        estaAbierta = !estaAbierta;
        ActualizarEstado();
        SonidoPuerta();
    }

 void ActualizarEstado()
{
    // 1. Cambiamos los dibujos
    if (hojaIzquierda != null) 
        hojaIzquierda.sprite = estaAbierta ? spriteAbiertaIzq : spriteCerradaIzq;
    
    if (hojaDerecha != null) 
        hojaDerecha.sprite = estaAbierta ? spriteAbiertaDer : spriteCerradaDer;
    
    if (GetComponent<Collider2D>() != null)
        GetComponent<Collider2D>().isTrigger = estaAbierta;

    Collider2D colIzq = hojaIzquierda.GetComponent<Collider2D>();
    Collider2D colDer = hojaDerecha.GetComponent<Collider2D>();

    if (colIzq != null) colIzq.isTrigger = estaAbierta;
    if (colDer != null) colDer.isTrigger = estaAbierta;
}
public void SonidoPuerta()
    {
        
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}