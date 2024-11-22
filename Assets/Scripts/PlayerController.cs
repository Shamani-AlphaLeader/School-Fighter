using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public float playerSpeed = 1f;

    public Vector2 playerDirection;

    private bool isWalking;

    private Animator playerAnimator;

    //Player olhando pra direita
    private bool playerFacingRight = true;
    void Start()
    {
        //Obtem e inicializa as propriedades do RigidBody2D
        playerRigidBody = GetComponent<Rigidbody2D>();

        //Obtem e inicializa as propriedades do animator
        playerAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        PlayerMove();
        UpdateAnimator();

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
        
        playerRigidBody.MovePosition(playerRigidBody.position + playerSpeed * Time.fixedDeltaTime * playerDirection);
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
}
