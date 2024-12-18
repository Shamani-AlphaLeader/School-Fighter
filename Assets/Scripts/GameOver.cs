using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel; // Painel de Fim de Jogo
    [SerializeField] private int playerHealth = 100; // Vida inicial do jogador
    private bool isGameOver = false; // Verifica se o jogo terminou

    // Método chamado quando o jogador recebe dano
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    // Método chamado quando o jogador morre
    private void Die()
    {
        Debug.Log("Jogador morreu!");
        gameOverPanel.SetActive(true); // Ativa o painel de fim de jogo
        Time.timeScale = 0; // Pausa o jogo
        isGameOver = true; // Marca o estado de fim de jogo
    }

    // Reiniciar o jogo
    private void RestartGame()
    {
        Time.timeScale = 1; // Restaura o tempo normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarrega a cena atual
    }

    // Verifica entradas de teclado
    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.Return)) // Verifica se Enter foi pressionado
        {
            RestartGame();
        }
    }

}
