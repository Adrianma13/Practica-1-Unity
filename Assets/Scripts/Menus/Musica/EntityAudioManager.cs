using UnityEngine;

public class EntityAudioManager : MonoBehaviour
{
    [Header("Componentes")]
    public AudioSource audioSource; // Un solo AudioSource para todo

    [Header("Clips de Sonido")]
    public AudioClip clipAtaque;
    public AudioClip clipHit;
    public AudioClip clipMuerte;
    public AudioClip clipPasos;
    

    // Métodos para ser llamados desde otros scripts
    public void PlayAttackSound() {
        PlaySound(clipAtaque);
    }

    public void PlayHitSound() {
        PlaySound(clipHit);
    }

    public void PlayDeathSound() {
        PlaySound(clipMuerte);
    }

    private void PlaySound(AudioClip clip) {
        if (clip != null && audioSource != null) {
            // Un poco de variación de tono para que no sea repetitivo
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(clip);
        }
    }
}