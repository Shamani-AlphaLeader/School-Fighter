using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    private AudioSource audioSource;
    private AudioClip splashSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Se pressionar qualquer tecla
        if (Input.GetKeyDown(KeyCode.Return))
        {
            audioSource.clip = splashSound;
            audioSource.Play();

            //Mudar de Cena
            StartCoroutine(CarregarFase("Fase1"));
        }
    }

    // Corrotina - Coroutine
    IEnumerator CarregarFase(string nomeFase)
    {
        // Iniciar a animação
        transition.SetTrigger("Start");
        
        // Esperar o tempo de animação
        yield return new WaitForSeconds(transitionTime);

        // Carregar a cena
        SceneManager.LoadScene(nomeFase);
    }

}
