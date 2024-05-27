using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashQuiz : MonoBehaviour
{
    public static TrashQuiz Instance;
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
        StartCoroutine(HandleQuizbenar1());
    }

    private IEnumerator HandleQuizbenar1()
    {
        StartCoroutine(QuizManager1.Instance.jawabanB(1.0f));
        yield return new WaitForSeconds(1.5f); // Tambahkan sedikit waktu untuk memastikan coroutine selesai
        Destroy(gameObject);
        quiz.SetActive(false);
        Debug.Log("jawaban benar");
    }

    public void Quizbenar2()
    {
        QuizManager2.Instance.CollectQuizTrash2();
        StartCoroutine(HandleQuizbenar2());
    }

    private IEnumerator HandleQuizbenar2()
    {
        StartCoroutine(QuizManager1.Instance.jawabanB(1.0f));
        yield return new WaitForSeconds(1.5f); // Tambahkan sedikit waktu untuk memastikan coroutine selesai
        Destroy(gameObject);
        quiz.SetActive(false);
        Debug.Log("jawaban benar");
    }

    public void Quizsalah1()
    {
        playerHealth.TakeDamage(10);
        StartCoroutine(HandleQuizsalah1());
    }

    private IEnumerator HandleQuizsalah1()
    {
        StartCoroutine(QuizManager1.Instance.jawabanS(1.0f));
        yield return new WaitForSeconds(1.5f); // Tambahkan sedikit waktu untuk memastikan coroutine selesai
        Destroy(gameObject);
        quiz.SetActive(false);
        Debug.Log("jawaban salah");
    }

}
