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
        if (knockback.GettingKnockedBack || isMovementStopped) return;

        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        if (moveDir.x > 0)
            FlipSprite(false);
        else if (moveDir.x < 0)
            FlipSprite(true);
    }

    private void FlipSprite(bool flipX)
    {
        GetComponent<SpriteRenderer>().flipX = flipX;
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = (targetPosition - (Vector2)transform.position).normalized;
    }

    public void StopMovement()
    {
        isMovementStopped = true;
        rb.velocity = Vector2.zero;
    }

    public void ResumeMovement()
    {
        isMovementStopped = false;
    }
}
