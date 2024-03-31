using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    public TrashCategory trashCategory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CollectOrganicTrash();
            Destroy(gameObject);
            Debug.Log("Sampah");
        }
    }
}
