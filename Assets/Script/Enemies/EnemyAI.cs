using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming,
        Stopped
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        StartCoroutine(RoamingCoroutine());
    }

    IEnumerator RoamingCoroutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
        // Setelah keluar dari loop while, cek jika statusnya Stopped
        if (state == State.Stopped)
        {
            // Implementasi logika berhenti di sini
            Debug.Log("Musuh berhenti di tempat.");
            enemyPathfinding.StopMovement(); // Memanggil fungsi StopMovement()
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    // Fungsi untuk menghentikan musuh
    public void StopEnemy()
    {
        state = State.Stopped;
        enemyPathfinding.StopMovement();
    }

    public void ResumeEnemy()
    {
        state = State.Roaming;
        StartCoroutine(RoamingCoroutine());
        enemyPathfinding.ResumeMovement(); // Memanggil fungsi ResumeMovement()
    }

    public void OnStopButtonClicked()
    {
        if (enemyPathfinding != null)
        {
            enemyPathfinding.StopMovement();
        }
        else
        {
            Debug.LogWarning("Enemy reference is not set.");
        }
    }

    public void OnResumeButtonClicked()
    {
        if (enemyPathfinding != null)
        {
            enemyPathfinding.ResumeMovement();
        }
        else
        {
            Debug.LogWarning("Enemy reference is not set.");
        }
    }
}
