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
    // this function will start in the game's initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCollider = GetComponent<Collider2D>(); // Make sure the Collider2D is attached to the character GameObject.
        life = Hearths.Length;
    }

    // this function is executed during gameplay
    private void Update()
    {
        // Get keyboard input for horizontal movement.
        float horizontalMovement = Input.GetAxis("Horizontal");

        // Calculate the movement direction.
        Vector3 moveDirection = new Vector3(horizontalMovement, 0f, 0f);

        // Update the character's position based on direction and speed.
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Check if the player pressed the space key to jump and is either grounded or has jumps remaining.
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if(life < 1){
            Destroy(Hearths[0].gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }else if(life < 2){
            Destroy(Hearths[1].gameObject);
        }else if(life < 3){
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
                Debug.Log("aqui");
                // Reduza uma vida.
                life--;

                // Defina o tempo de espera antes de permitir outra colisão com o mesmo inimigo.
                canTakeDamage = false;
                StartCoroutine(ResetDamageCooldown());
            }
        }

    }

    private IEnumerator ResetDamageCooldown()
    {
        yield return new WaitForSeconds(1.0f); // Espere por 1 segundo (ou o tempo que você desejar).
        canTakeDamage = true; // Permite outra colisão com inimigo.
    }

}
