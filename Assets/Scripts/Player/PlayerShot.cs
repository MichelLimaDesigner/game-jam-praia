using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab of the bullet.
    public Transform firePoint; // Point from where the bullets will be fired.
    public float bulletSpeed = 10.0f; // Bullet speed.
    public float bulletLifetime = 2.0f; // Bullet lifetime in seconds.

    private bool facingRight = true; // Flag to control the character's direction.

    [SerializeField] AudioSource SFXSource;

    [Header("----- Audio Clip -----")]
    public AudioClip shoot;

    // audio control
    AudioManager audioManager;
    // Update is called once per frame
    void Update()
    {
        // Detect the direction of the character's movement.
        float horizontalInput = Input.GetAxis("Horizontal");
        
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }

        // Detect the input to shoot (e.g., by pressing the "Fire1" button).
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }
    }

    void Flip()
    {
        // Invert the character's direction and scale on the horizontal axis.
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Shoot()
    {
        // Create an instance of the bullet at the fire point.
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Get the shooting direction based on the character's direction.
        Vector2 shootDirection = facingRight ? Vector2.right : Vector2.left;

        // Set the bullet's velocity in the correct direction.
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
        PlaySFX(shoot);
        // Destroy the bullet after a specified time period (bulletLifetime).
        Destroy(bullet, bulletLifetime);
    }

    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }
    
}
