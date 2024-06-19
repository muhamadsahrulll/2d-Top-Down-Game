using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactDialog : MonoBehaviour
{
    [SerializeField] private GameObject btnDialogue;
    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.CompareTag("Player"))
        {
            btnDialogue.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D colider)
    {
        if (colider.CompareTag("Player"))
        {
            btnDialogue.gameObject.SetActive(false);
        }
    }
}
