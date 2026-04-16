using UnityEngine;
using System.Collections.Generic;

public class CombateSlime : CombateEnemigo 
{
    [Header("Configuración Abanico (Slime)")]
    [SerializeField] private Transform[] controladoresAtaque;
    [SerializeField] private float radioPuntosAbanico = 0.4f;

    // EjecutarDaño específico para el Slime (Llamado por Animation Event)
    public override void EjecutarDaño() 
    {
        List<Collider2D> golpeados = new List<Collider2D>();

        foreach (Transform punto in controladoresAtaque)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(punto.position, radioPuntosAbanico, capaJugador);
            foreach (Collider2D col in hits)
            {
                if (!golpeados.Contains(col))
                {
                    if (col.TryGetComponent<VidaJugador>(out VidaJugador vida))
                        vida.TomarDaño(daño);
                    golpeados.Add(col);
                }
            }
        }
    }
}