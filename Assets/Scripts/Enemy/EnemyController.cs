using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;
    GameObject player;
    public bool enemyFindPlayer;

     //Ponto para o qual o personagem irá se mover
    private GameObject Point;
    //Variável NavMeshAgent Para configurar A movimentação do personagem
    private NavMeshAgent agent;
    //Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //anim = GetComponentInChildren<Animator>();

        //Pega o Componente NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        //Variaveis setadas como False para Não utilizar os eixos Y Baseado em 3 dimensões
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Encontra o ponto Na cena
        Point = GameObject.FindGameObjectWithTag("Player");
        if(enemyFindPlayer){
            //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            agent.SetDestination(Point.transform.position);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            enemyFindPlayer = true;
            player = GameObject.FindGameObjectWithTag("Player");
            //Destroy(gameObject);
        }
    }
}