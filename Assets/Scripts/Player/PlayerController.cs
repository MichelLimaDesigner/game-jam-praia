using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidadeMovimento = 5.0f; // Velocidade de movimento do personagem.
    public float forcaPulo = 200.0f; // Força do pulo.
    
    private Rigidbody2D rb;
    private Collider2D chaoCollider; // Collider do chão.
    private bool estaNoChao; // Flag para verificar se o personagem está no chão.

    // this function will start in the init of game
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        chaoCollider = GetComponent<Collider2D>(); // Certifique-se de que o Collider2D está anexado ao GameObject do personagem.
    }

    // está funcao é executada durante o jogo
    private void Update()
    {
        // Obtém a entrada do teclado para a movimentação horizontal.
        float movimentoHorizontal = Input.GetAxis("Horizontal");

        // Calcula a direção do movimento.
        Vector3 direcaoMovimento = new Vector3(movimentoHorizontal, 0f, 0f);

        // Atualiza a posição do personagem com base na direção e na velocidade.
        transform.position += direcaoMovimento * velocidadeMovimento * Time.deltaTime;

        // Verifica se o jogador pressionou a tecla de espaço para pular.
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            Pular();
        }
    }


    private void Pular()
    {
        // Aplica uma força para fazer o personagem pular.
        rb.AddForce(Vector2.up * forcaPulo);
    }

    private void FixedUpdate()
    {
        // Verifica se o personagem está no chão usando um Raycast.
        // Isso requer que um Collider2D do chão esteja configurado em uma camada de colisão apropriada.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, chaoCollider.bounds.extents.y + 0.1f);
        estaNoChao = hit.collider != null;
    }
}
