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

    void Start()
    {
        // Al empezar, nos aseguramos de que los sprites coincidan con el estado
        ActualizarEstado();
    }

    public void Interactuar()
    {
        estaAbierta = !estaAbierta;
        ActualizarEstado();
    }

 void ActualizarEstado()
{
    // 1. Cambiamos los dibujos
    if (hojaIzquierda != null) 
        hojaIzquierda.sprite = estaAbierta ? spriteAbiertaIzq : spriteCerradaIzq;
    
    if (hojaDerecha != null) 
        hojaDerecha.sprite = estaAbierta ? spriteAbiertaDer : spriteCerradaDer;
    
    // 2. HACER QUE SE PUEDA PASAR
    // Primero: El collider del PADRE (el que detecta la E)
    // Lo ponemos como trigger para que no choque con el cuerpo del jugador
    if (GetComponent<Collider2D>() != null)
        GetComponent<Collider2D>().isTrigger = estaAbierta;

    // Segundo: Los colliders de las HOJAS
    Collider2D colIzq = hojaIzquierda.GetComponent<Collider2D>();
    Collider2D colDer = hojaDerecha.GetComponent<Collider2D>();

    if (colIzq != null) colIzq.isTrigger = estaAbierta;
    if (colDer != null) colDer.isTrigger = estaAbierta;
}
}