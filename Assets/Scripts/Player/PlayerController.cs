using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Character movement speed.
    public float jumpForce = 200.0f; // Jump force.
    public int maxJumps = 3; // Maximum number of jumps.

    private Rigidbody2D rb;
    private Collider2D groundCollider; // Ground collider.
    private bool isGrounded; // Flag to check if the character is grounded.

    public GameObject[] Hearths;
    private int life;
    private bool canTakeDamage = true; // Flag para controlar o tempo entre as colisões com inimigos.

    public PlayerAnimationController playerAnim;
    public Material redMaterial; // Material vermelho.

    private Material originalMaterial; // Material original do personagem.
    private bool isRed = false; // Flag para verificar se o personagem está vermelho.

    // this function will start in the game's initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCollider = GetComponent<Collider2D>(); // Make sure the Collider2D is attached to the character GameObject.
        life = Hearths.Length;

        // Salva o material original do personagem
        originalMaterial = GetComponent<Renderer>().material;
    }

    // this function is executed during gameplay
    private void Update()
    {
        // Get keyboard input for horizontal movement.
        float horizontalMovement = Input.GetAxis("Horizontal");

        // Calculate the movement direction.
        Vector3 moveDirection = new Vector3(horizontalMovement, 0f, 0f);

        if (Mathf.Abs(horizontalMovement) > 0.1f)
        {
            playerAnim.PlayerAnimation("playerWalk");
        }

        // Update the character's position based on direction and speed.
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Check if the player pressed the space key to jump and is either grounded or has jumps remaining.
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            Jump();
            playerAnim.PlayerAnimation("playerJump");
        }

        if (life < 1)
        {
            Destroy(Hearths[0].gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }
        else if (life < 2)
        {
            Destroy(Hearths[1].gameObject);
        }
        else if (life < 3)
        {
            Destroy(Hearths[2].gameObject);
        }
    }

    private void Jump()
    {
        // Apply a force to make the character jump.
        rb.velocity = new Vector2(rb.velocity.x, 0f); // Reset the vertical velocity before jumping.
        rb.AddForce(Vector2.up * jumpForce);
    }

    private void FixedUpdate()
    {
        // Check if the character is grounded by checking for collisions with objects tagged as 'ground'.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, groundCollider.bounds.extents.y + 0.1f);

        // Reset the grounded flag.
        isGrounded = false;

        // Check each collider to see if it's tagged as 'ground'.
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Ground"))
            {
                isGrounded = true;
                break; // Exit the loop as soon as we find one 'ground' object.
            }
            if (collider.CompareTag("Enemy") && canTakeDamage)
            {
                // Reduza uma vida.
                life--;
                // Define o material vermelho quando o jogador perder uma vida.
                GetComponent<Renderer>().material = redMaterial;
                // Defina o tempo de espera antes de permitir outra colisão com o mesmo inimigo.
                canTakeDamage = false;
                StartCoroutine(ResetDamageCooldown());

                 // Alterna a cor do personagem para vermelho.
                if (!isRed)
                {
                    GetComponent<Renderer>().material = redMaterial;
                    isRed = true;
                    StartCoroutine(ResetRedCooldown());
                }
            }
        }
    }

    private IEnumerator ResetDamageCooldown()
    {
        yield return new WaitForSeconds(1.0f); // Espere por 1 segundo (ou o tempo que você desejar).
        canTakeDamage = true; // Permite outra colisão com inimigo.
    }

    private IEnumerator ResetRedCooldown()
    {
        yield return new WaitForSeconds(0.5f); // Espere por 1 segundo (ou o tempo que você desejar).
        // Volta à cor original após o tempo de espera.
        GetComponent<Renderer>().material = originalMaterial;
        isRed = false;
    }
}
