using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instancia;

    void Awake()
    {
        // Sistema para que la música sea continua y no se duplique
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CambiarMusica(AudioClip nuevaMusica)
    {
        AudioSource fuente = GetComponent<AudioSource>();

        // Solo cambiamos si es una canción distinta a la que suena
        if (fuente.clip != nuevaMusica)
        {
            fuente.Stop();
            fuente.clip = nuevaMusica;
            fuente.Play();
        }
    }
}
