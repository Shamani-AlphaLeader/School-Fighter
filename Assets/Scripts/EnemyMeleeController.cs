using UnityEngine;

public class EnemyMeleeController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    // Variavel que indica se o inimigo esta vivo
    public bool isDead;

    // Variaveis para controlar o lado em que o inimigo esta virado
    public bool facingRight;
    public bool previousDirectionRight;

    // Variavel para armazenar a posicao do player
    private Transform target;

    //Variaveis par movimentacao do inimigo
    private float enemySpeed = 0.3f;
    private float currentSpeed;

    private bool isWalking;

    private float horizontalForce;
    private float verticalForce;

    // Variavel usada para 
    private float walkTimer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Buscar o player e armazenar a sua posicao
        target = FindAnyObjectByType<PlayerController>().transform;

        // Inicializar a velocidade do inimigo
        currentSpeed = enemySpeed;
    }

    
    void Update()
    {
        // Verificar se o player esta para a direita ou para a esquerda
        // Determinar o lado em que o inimigo ficara virado
        if (target.position.x < transform.position.x)
        {
            facingRight = false;
        }
        else
        { 
        facingRight = true;
        }

        // Se facingRight for TRUE, vamos virar o inimigo em180 graus no eixo Y
        // Senao vamos virar o inimigo para a esquerda
        
        // Se o player esta a direita e a posicao anterior NAO era direita (inimigo estava olhando para a esquerda)
        if (facingRight && !previousDirectionRight)
        {
            this.transform.Rotate(0, 180, 0);
            previousDirectionRight = true;
        }

        // Se o player NAO esta a direita e a posicao anterior ERA direita (inimigo estava olhando para a direita)
        if (!facingRight && previousDirectionRight)
        {
            this.transform.Rotate(0, -180, 0);
            previousDirectionRight = false;
        }

        // Inicia o timer do caminhar do inimigo
        walkTimer += Time.deltaTime;

        // Gerenciar a animacao do inimigo
        if (horizontalForce ==  0 && verticalForce == 0)
        {
            isWalking = false;
        }
        else
        {
            isWalking = true;
        }

        // Atualiza o Animator
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        // MOVIMENTACAO

        // Variavel para armazenar a distancia entre o inimigo e o player
        Vector3 targetDistance = target.position - this.transform.position;

        //Determina se a forca horizontal deve ser negativa ou positiva
        // 5 / 5 = 1
        // -5 / 5 = -1
        horizontalForce = targetDistance.x / Mathf.Abs(targetDistance.x);

        // Entre 1 e 2 sera feita uma definicao de direcao vertical
        if (walkTimer >= Random.Range(1f, 2f))
        {
            verticalForce = Random.Range(-1, 2);

            // Zera o timer de movimentacao para andar verticalmente novamente daqui a +- 1 segundo
            walkTimer = 0;
        }

        // Caso esteja perto do player, para a velocidade
        if (Mathf.Abs(targetDistance.x) < 0.2f)
        {
            horizontalForce = 0;
        }

        // Aplica velocidade no inimigo fazendo-o movimentar
        rb.linearVelocity = new Vector2(horizontalForce * currentSpeed, verticalForce * currentSpeed);
    }

    void UpdateAnimator()
    {
        animator.SetBool("isWalking", isWalking);
    }
}
