using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public float playerSpeed = 1f;
public float currentSpeed = 1f;

    public Vector2 playerDirection;

    private bool isWalking;

    private Animator playerAnimator;

    //Player olhando pra direita
    private bool playerFacingRight = true;

    //Variavel contadora
    private int punchCount;

    //Tempo de ataque
    private float timeCross = 1.20f;

    private bool comboControl;

    // Indica se o plyer esta morto
    private bool isDead;

    // Propriedades para UI
    public int maxHealth = 10;
    public int currentHealth;
    public Sprite playerImage;

    void Start()
    {
        //Obtem e inicializa as propriedades do RigidBody2D
        playerRigidBody = GetComponent<Rigidbody2D>();

        //Obtem e inicializa as propriedades do animator
        playerAnimator = GetComponent<Animator>();
        currentSpeed = playerSpeed;

        // Inicia a vida do player
        currentHealth = maxHealth;
    }

    
    void Update()
    {
        PlayerMove();
        UpdateAnimator();


        if (Input.GetKeyDown(KeyCode.X))
        {
            
            


                if (punchCount < 2)
                {
                    PlayerJab();
                    punchCount++;
                    if (!comboControl)
                    {
                        //Iniciar Temporizador
                        StartCoroutine(CrossController());

                    }
                }

                else if (punchCount >= 2) 
                {
                PlayerCross();
                    punchCount = 0;
                }
            

        }

        //Parando o temporizador
        StopCoroutine(CrossController());
    }

    //O FixedUpdate sera utilizado para implementar fisica no jogo
    private void FixedUpdate()
    {
        //Verificar se o player esta em movimento
        //Por ter uma execucao padronizada em diferentes dispositivos
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            isWalking = true;
        }
        else
        { 
        isWalking= false;
        }
        
        playerRigidBody.MovePosition(playerRigidBody.position + currentSpeed * Time.fixedDeltaTime * playerDirection);
    }

    void PlayerMove() 
    {
        //Pega a entrada do jogador, e cria um Vector2 para usar no playerDirection
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Se o player vai para a ESQUERDA e esta olhando para a DIREITA
        if (playerDirection.x < 0 && playerFacingRight)
        {
            Flip();
        }

        //Se o player vai para a DIREITA  e esta olhando para a ESQUERDA
        else if (playerDirection.x > 0 && !playerFacingRight)
        {
            Flip();
        }
    }

    void UpdateAnimator() 
    {
        //Definir o valor da propriedade igual ao valor do parametro do Animator, igual a propriedade isWalking
        playerAnimator.SetBool("isWalking", isWalking);
    }

    void Flip() 
    {
    //Vai girar o sprite do player em 180 graus no eixo Y
    playerFacingRight = !playerFacingRight;

        //Girar o sprite em 180 graus no eixo Y
        //X,Y,Z
        transform.Rotate(0, 180, 0);
    }

    void PlayerJab()
    {
        //Acessa a animacao do Jab
        //Ativa o gatilho de ataque Jab
        playerAnimator.SetTrigger("isJab");
    }

    void PlayerCross()
    {
        //Acessa a animacao do Cross
        //Ativa o gatilho de ataque Cross
        playerAnimator.SetTrigger("isCross");
    }

    IEnumerator CrossController()
    {
        comboControl = true;

        yield return new WaitForSeconds(timeCross);
        punchCount = 0;

        comboControl = false;
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = playerSpeed;
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            playerAnimator.SetTrigger("HitDamage");
            FindFirstObjectByType<UIManager>().UpdatePlayerHealth(currentHealth);
        }
    }
}
