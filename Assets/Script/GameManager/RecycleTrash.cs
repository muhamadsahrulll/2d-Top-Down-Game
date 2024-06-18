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
                if (trashCategory.categoryName == "Plastik" || trashCategory.categoryName == "Gunting" || trashCategory.categoryName == "Tali" || trashCategory.categoryName == "BotolDR" || trashCategory.categoryName == "Bunga")
                {
                    RecycleManager2.Instance.CollectRecycleTrash(trashCategory.categoryName);
                }

                Destroy(gameObject);
                Debug.Log($"Sampah {trashCategory.categoryName}");
            }
        }
    }
}
