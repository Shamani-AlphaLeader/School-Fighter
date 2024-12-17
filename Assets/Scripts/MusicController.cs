using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Classe responsavel por controlar qualquer tipo de audio
    private AudioSource audioSource;

    // AudioClip é o arquivo de audio que sera executado
    public AudioClip levelMusic;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Ao iniciar o MusicController, inicia a musica da fase
        PlayMusic(levelMusic);
    }

    public void PlayMusic(AudioClip music)
    {
        // Define a musica ou som que sera reproduzido
        audioSource.clip = music;
        
        // Reproduz o som ou musica
        audioSource.Play();
    }
    void Update()
    {
        
    }
}
