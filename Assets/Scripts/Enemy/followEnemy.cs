using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followEnemy : MonoBehaviour
{
    private Transform playerPosition;
    public float speedEnemy;
    public float followDistance = 5.0f; // A distância a partir da qual o inimigo começa a seguir o jogador.

    private bool isFollowing = false;
    private bool isFacingRight = true; // Flag para verificar a direção em que o inimigo está voltado.

    private SpriteRenderer enemySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerPosition.position);

        if (distanceToPlayer <= followDistance)
        {
            isFollowing = true;
        }
        else
        {
            isFollowing = false;
        }

        if (isFollowing)
        {
            followPlayer();
        }
    }

    private void followPlayer()
    {
        Vector3 moveDirection = (playerPosition.position - transform.position).normalized;

        // Mantenha o movimento apenas no eixo X.
        Vector3 newPosition = transform.position + new Vector3(moveDirection.x * speedEnemy * Time.deltaTime, 0, 0);

        // Atribua a nova posição ao transform do inimigo.
        transform.position = newPosition;

        // Verifique a direção em que o inimigo está voltado e ajuste a escala conforme necessário.
        if (moveDirection.x > 0 && !isFacingRight)
        {
            FlipEnemy();
        }
        else if (moveDirection.x < 0 && isFacingRight)
        {
            FlipEnemy();
        }
    }

    private void FlipEnemy()
    {
        // Inverte a escala horizontal do inimigo para virar para a esquerda ou direita.
        isFacingRight = !isFacingRight;
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }
}
