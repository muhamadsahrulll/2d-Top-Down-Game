using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController2 : MonoBehaviour
{
    public TrashCategory trashCategory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlayPickUp();
            InorganicManager.Instance.CollectInorganicTrash();
            Destroy(gameObject);
            Debug.Log("Sampah anorganic");
        }
    }
}
