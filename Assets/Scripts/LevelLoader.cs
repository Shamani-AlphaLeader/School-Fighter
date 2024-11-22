using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Se pressionar qualquer tecla
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            //Mudar de cena
            StartCoroutine(CarregarFase("Fase01"));
        }
    }
    //Co-rotina - Coroutine
    IEnumerator CarregarFase(string nomeFase) 
    {
        //Iniciar a animacao
        transition.SetTrigger("Start");

        //Esperar o tempo de animacao
    yield return new WaitForSeconds(1f);

        //Carregar a cena
        SceneManager.LoadScene(nomeFase);
    }

}
