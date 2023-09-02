using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] points;

    private int currentPoint;

    [SerializeField] private float speed;

    private float lastPositionX;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code can go here
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
        FlipPicture();
    }

    private void MoveEnemy()
    {
        if (points.Length == 0)
        {
            return; // Check if the array is empty to avoid errors.
        }

        // Move the enemy towards the current target point.
        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint].position, speed * Time.deltaTime);

        // Check if the enemy has reached the current target point.
        if (transform.position == points[currentPoint].position)
        {
            currentPoint += 1;
            lastPositionX = transform.localPosition.x;

            // Make sure currentPoint stays within valid array bounds.
            if (currentPoint >= points.Length)
            {
                currentPoint = 0;
            }
        }
    }

    private void FlipPicture()
    {
        // Flip the picture horizontally based on the enemy's movement direction.
        if (transform.localPosition.x < lastPositionX)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (transform.localPosition.x > lastPositionX)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
