using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject[] trashPrefabs;   // Array prefab untuk objek sampah
    public int numberOfTrash = 4;       // Jumlah objek sampah yang akan ditempatkan
    public float leftBound = -10f;      // Batas kiri area spawn
    public float rightBound = 10f;      // Batas kanan area spawn
    public float topBound = 5f;         // Batas atas area spawn
    public float bottomBound = -5f;     // Batas bawah area spawn
    public float colliderCheckRadius = 0.5f;  // Radius pengecekan Collider 2D
    public LayerMask ignoreLayer;       // Layer untuk collider yang diabaikan
    public LayerMask foregroundLayer;   // Layer untuk collider foreground yang dihindari

    private List<Vector2> usedPositions = new List<Vector2>();

    void Start()
    {
        Debug.Log("TrashSpawner Start");

        for (int i = 0; i < trashPrefabs.Length; i++)
        {
            Vector2 position = GetValidPosition();
            Debug.Log($"Position Found: {position}");

            if (position != Vector2.zero)
            {
                GameObject trashPrefab = trashPrefabs[i];
                Instantiate(trashPrefab, position, Quaternion.identity);
                Debug.Log($"Trash instantiated at: {position}");
            }
            else
            {
                Debug.Log($"No valid position found for trash prefab index: {i}");
            }
        }
    }

    Vector2 GetValidPosition()
    {
        for (int i = 0; i < 100; i++)  // Ulangi sampai 100 kali
        {
            Vector2 randomPosition = new Vector2(
                Mathf.Round(Random.Range(leftBound, rightBound)),
                Mathf.Round(Random.Range(bottomBound, topBound))
            );

            Debug.Log($"Random Position: {randomPosition}");

            if (IsPositionValid(randomPosition))
            {
                usedPositions.Add(randomPosition);
                return randomPosition;
            }
        }

        return Vector2.zero;  // Mengembalikan (0, 0) jika tidak ditemukan posisi yang valid
    }

    bool IsPositionValid(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, colliderCheckRadius);
        foreach (Collider2D collider in colliders)
        {
            if (!collider.isTrigger &&
                collider.gameObject.layer != LayerMask.NameToLayer("Camera") &&
                collider.gameObject.layer != LayerMask.NameToLayer("Foreground")) // Cek jika collider bukan trigger, bukan layer yang diabaikan dan bukan layer foreground
            {
                Debug.Log($"Position {position} invalid due to collider: {collider.name}");
                return false;
            }
        }

        // Pastikan posisi belum digunakan
        if (usedPositions.Contains(position))
        {
            Debug.Log($"Position {position} already used");
            return false;
        }

        Debug.Log($"Position {position} is valid");
        return true;
    }
}
