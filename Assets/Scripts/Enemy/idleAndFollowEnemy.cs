using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleAndFollowEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    private int currentPoint;
    [SerializeField] private float speed;
    private float lastPositionX;
    public float followDistance = 5.0f; // A distância a partir da qual o inimigo começa a seguir o jogador.
    private Transform playerPosition;
    private bool isFacingRight = true; // Flag para verificar a direção em que o inimigo está voltado.
    private SpriteRenderer enemySpriteRenderer;
    private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerPosition.position);

        // Se a distância for menor que a distância de seguir, siga o jogador.
        if (distanceToPlayer <= followDistance)
        {
            followPlayer();
        }
        else
        {
            MoveEnemy();
        }
    }

    private void followPlayer()
    {
        Vector3 moveDirection = (playerPosition.position - transform.position).normalized;

        // Mantenha o movimento apenas no eixo X e Y.
        Vector3 newPosition = transform.position + new Vector3(moveDirection.x * speed * Time.deltaTime, moveDirection.y * speed * Time.deltaTime, 0);

        // Atribua a nova posição ao transform do inimigo.
        transform.position = newPosition;

        // Verifique a direção em que o inimigo está voltado e ajuste a escala conforme necessário.
        if (moveDirection.x > 0 && !isFacingRight)
        {
            FlipPicture(true); // Mudança para a direita
        }
        else if (moveDirection.x < 0 && isFacingRight)
        {
            FlipPicture(false); // Mudança para a esquerda
        }
    }

    private void MoveEnemy()
    {
        if (points.Length == 0)
        {
            return; // Verifique se o array está vazio para evitar erros.
        }

        // Obtenha a direção atual do movimento.
        Vector3 moveDirection = (points[currentPoint].position - transform.position).normalized;

        // Verifique se o inimigo mudou de direção.
        if (moveDirection.x < 0 && isFacingRight)
        {
            FlipPicture(false); // Mudança para a esquerda
        }
        else if (moveDirection.x > 0 && !isFacingRight)
        {
            FlipPicture(true); // Mudança para a direita
        }

        // Atualize a posição anterior para a próxima verificação.
        previousPosition = transform.position;

        // Move o inimigo em direção aos pontos.
        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint].position, speed * Time.deltaTime);

        // Verifique se o inimigo alcançou o ponto atual.
        if (transform.position == points[currentPoint].position)
        {
            currentPoint += 1;
            lastPositionX = transform.localPosition.x;

            // Certifique-se de que currentPoint permaneça dentro dos limites válidos do array.
            if (currentPoint >= points.Length)
            {
                currentPoint = 0;
            }
        }
    }

    private void FlipPicture(bool flipRight)
    {
        // Inverte a escala horizontal do inimigo para virar para a esquerda ou direita.
        isFacingRight = flipRight;
        Vector3 enemyScale = transform.localScale;
        enemyScale.x = isFacingRight ? 1 : -1;
        transform.localScale = enemyScale;
    }
}
