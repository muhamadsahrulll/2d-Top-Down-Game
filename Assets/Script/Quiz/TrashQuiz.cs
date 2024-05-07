using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashQuiz : MonoBehaviour
{
    public TrashCategory trashCategory;
    public GameObject quiz;
    public PlayerHealth playerHealth;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            quiz.SetActive(true);
            //InorganicManager.Instance.CollectInorganicTrash();
            //Destroy(gameObject);
            Debug.Log("kuis");
        }
    }

    public void Quizbenar1()
    {
        QuizManager1.Instance.CollectQuizTrash();
        Destroy(gameObject);
        quiz.SetActive(false);
        Debug.Log("jawaban benar");
    }

    public void Quizsalah1()
    {
        playerHealth.TakeDamage(10);
        Destroy(gameObject);
        quiz.SetActive(false);
        Debug.Log("jawaban salah");
    }

    
}
