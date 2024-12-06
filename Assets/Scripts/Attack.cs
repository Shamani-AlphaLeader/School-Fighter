using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ao colidir, salva na variavel enemy, o inimigo que foi colidido
        EnemyMeleeController enemy = collision.GetComponent<EnemyMeleeController>();

        // Ao colidir, salva na variavel player, o player que foi atingido
        PlayerController player = collision.GetComponent<PlayerController>();

        if (enemy != null )
        {
            // Inimigo recebe dano
            enemy.TakeDamage(damage);
        }

        if (player != null)
        {
            // player recebe dano
            player.TakeDamage(damage);
        }
    }
}
