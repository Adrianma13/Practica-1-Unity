using UnityEngine;

public class CombateVampiro : CombateEnemigo 
{
    [Header("Ataque a Distancia (Jefe)")]
    [SerializeField] private GameObject prefabProyectil;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private int cantidadProyectiles = 5;
    [SerializeField] private float anguloApertura = 60f;

    // Nuestro candado de seguridad
    private bool yaDisparo = false; 

    protected override void Update()
    {
        base.Update(); // Ejecuta el cronómetro del script padre

        // Si el vampiro puede moverse (es decir, no está atacando), quitamos el candado
        if (logicaMovimiento.puedeMoverse)
        {
            yaDisparo = false;
        }
    }

    public override void EjecutarDaño() 
    {
        // Si el candado está cerrado, cancelamos la función instantáneamente
        if (yaDisparo) return; 
        
        // Cerramos el candado para que no se repita en esta animación
        yaDisparo = true; 

        if (jugador == null || prefabProyectil == null || puntoDisparo == null) return;

        Vector2 direccionCentral = (jugador.position - puntoDisparo.position).normalized;
        float anguloBase = Mathf.Atan2(direccionCentral.y, direccionCentral.x) * Mathf.Rad2Deg;

        float anguloInicial = anguloBase - (anguloApertura / 2f);
        float pasoAngulo = anguloApertura / (cantidadProyectiles - 1);

        for (int i = 0; i < cantidadProyectiles; i++)
        {
            float anguloActual = anguloInicial + (pasoAngulo * i);
            Instantiate(prefabProyectil, puntoDisparo.position, Quaternion.Euler(0, 0, anguloActual));
        }
    }
}