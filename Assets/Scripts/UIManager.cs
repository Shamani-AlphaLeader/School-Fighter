using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthBar;
    public Image playerImage;

    public GameObject enemyUI;
    public Slider enemyHealthBar;
    public Image enemyImage;

    // Objeto para armazenar os dados do player
    private PlayerController player;

    // Timers e controles do enemyUI
    [SerializeField] private float enemyUITime = 4f;
    private float enemyTimer;
    void Start()
    {
        // Obtem os dados do player
        player = FindFirstObjectByType<PlayerController>();

        // Define o valor maximo da barra de vida igual ao maximo da vida do player
        playerHealthBar.maxValue = player.maxHealth;

        // Inicia a HealthBar cheia
        playerHealthBar.value = playerHealthBar.maxValue;

        // Define a imagem do player
        playerImage.sprite = player.playerImage;
    }

    
    void Update()
    {
        enemyTimer += Time.deltaTime;

        // Se o tempo limite for atingido, oculta a UI e reseta o timer
        if (enemyTimer >= enemyUITime)
        { 
            enemyUI.SetActive(false);
            enemyTimer = 0;
        }
    }

    public void UpdatePlayerHealth(int amount)
    {
        playerHealthBar.value = amount;
    }

    public void UpdateEnemyUI(int maxHealth, int currentHealth, Sprite image)
    {
        enemyHealthBar.maxValue = maxHealth;
        enemyHealthBar.value = currentHealth;
        enemyImage.sprite = image;

        // Zera o timer para comecar a contar 4 segundos
        enemyTimer = 0;

        // Habilita a enemyUI, deixando-a visivel
        enemyUI.SetActive(true);
    }

    
}
