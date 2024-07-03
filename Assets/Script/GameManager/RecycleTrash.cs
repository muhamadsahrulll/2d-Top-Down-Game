using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleTrash : MonoBehaviour
{
    public TrashCategory trashCategory;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.CompareTag("Player"))
            {
                if (trashCategory.categoryName == "Plastik" || trashCategory.categoryName == "Gunting" || trashCategory.categoryName == "Gunting2" || trashCategory.categoryName == "Tali" || trashCategory.categoryName == "BotolDR" || trashCategory.categoryName == "Bunga")
                {
                    AudioManager.instance.PlayPickUp();
                    // Panggil CollectRecycleTrash pada kedua instance RecycleManager dan RecycleManager2 jika mereka ada
                    if (RecycleManager.Instance != null)
                    {
                        RecycleManager.Instance.CollectRecycleTrash(trashCategory.categoryName);
                    }

                    if (RecycleManager2.Instance != null)
                    {
                        RecycleManager2.Instance.CollectRecycleTrash(trashCategory.categoryName);
                    }
                }

                Destroy(gameObject);
                Debug.Log($"Sampah {trashCategory.categoryName}");
            }
        }
    }
}
