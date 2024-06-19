using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming,
        ChasingPlayer,
        Stopped
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;
    private Transform playerTransform;
    private float chaseRadius = 5f;
    private float stopChaseRadius = 7f;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Pastikan player memiliki tag "Player"
    }

    private void Start()
    {
        StartCoroutine(RoamingCoroutine());
        StartCoroutine(DetectPlayerCoroutine());
    }

    IEnumerator RoamingCoroutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
        if (state == State.Stopped)
        {
            Debug.Log("Musuh berhenti di tempat.");
            enemyPathfinding.StopMovement();
        }
    }

    IEnumerator DetectPlayerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (state == State.Stopped) continue;

            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer < chaseRadius)
            {
                state = State.ChasingPlayer;
                StartCoroutine(ChasePlayerCoroutine());
            }
            else if (state == State.ChasingPlayer && distanceToPlayer > stopChaseRadius)
            {
                state = State.Roaming;
                StartCoroutine(RoamingCoroutine());
            }
        }
    }

    IEnumerator ChasePlayerCoroutine()
    {
        while (state == State.ChasingPlayer)
        {
            enemyPathfinding.MoveTo(playerTransform.position);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public void StopEnemy()
    {
        state = State.Stopped;
        enemyPathfinding.StopMovement();
    }

    public void ResumeEnemy()
    {
        state = State.Roaming;
        StartCoroutine(RoamingCoroutine());
        enemyPathfinding.ResumeMovement();
    }
}
