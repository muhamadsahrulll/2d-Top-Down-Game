using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Knockback knockback;

    private bool isMovementStopped = false;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (knockback.GettingKnockedBack || isMovementStopped) { return; }
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        // Memastikan sprite menghadap ke arah yang benar
        if (moveDir.x > 0)
        {
            // Menghadap ke kanan
            FlipSprite(false);
        }
        else if (moveDir.x < 0)
        {
            // Menghadap ke kiri
            FlipSprite(true);
        }
    }

    private void FlipSprite(bool flipX)
    {
        // Mengatur sprite flipX
        GetComponent<SpriteRenderer>().flipX = flipX;
    }

    public void MoveTo(Vector2 targetposition)
    {
        moveDir = (targetposition - (Vector2)transform.position).normalized;
    }

    // Metode untuk menghentikan pergerakan musuh
    public void StopMovement()
    {
        isMovementStopped = true;
        rb.velocity = Vector2.zero;
    }

    // Metode untuk melanjutkan pergerakan musuh
    public void ResumeMovement()
    {
        isMovementStopped = false;
    }
}
