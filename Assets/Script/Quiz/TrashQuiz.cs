using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashQuiz : MonoBehaviour
{
    public static TrashQuiz Instance;
    public TrashCategory trashCategory;
    public GameObject quiz;
    public PlayerHealth playerHealth;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            quiz.SetActive(true);
            AudioManager.instance.PlaySoal();
            player.SetActive(false);
            Debug.Log("kuis");
        }
    }

    public void Quizbenar1()
    {
        if (QuizManager1.Instance != null)
        {
            QuizManager1.Instance.CollectQuizTrash();
            AudioManager.instance.PlaySfxBenar();
            StartCoroutine(HandleQuizbenar(QuizManager1.Instance));
        }
    }

    public void Quizbenar2()
    {
        if (QuizManager2.Instance != null)
        {
            QuizManager2.Instance.CollectQuizTrash2();
            AudioManager.instance.PlaySfxBenar();
            StartCoroutine(HandleQuizbenar(QuizManager2.Instance));
        }
    }

    private IEnumerator HandleQuizbenar(object quizManager)
    {
        if (quizManager is QuizManager1 quizManager1)
        {
            StartCoroutine(quizManager1.jawabanB(1.0f));
        }

        if (quizManager is QuizManager2 quizManager2)
        {
            StartCoroutine(quizManager2.jawabanB(1.0f));
        }

        yield return new WaitForSeconds(1.5f); // Tambahkan sedikit waktu untuk memastikan coroutine selesai
        Destroy(gameObject);
        player.SetActive(true);
        quiz.SetActive(false);

        Debug.Log("jawaban benar");
    }

    public void Quizsalah1()
    {
        if (QuizManager1.Instance != null)
        {
            playerHealth.TakeDamage(10);
            AudioManager.instance.PlaySfxSalah();
            QuizManager1.Instance.AnswerWrong();
            StartCoroutine(HandleQuizsalah(QuizManager1.Instance));
        }
        else
        {
            Debug.LogWarning("QuizManager1.Instance is null. Quizsalah1() cannot proceed.");
        }
    }

    public void Quizsalah2()
    {
        if (QuizManager2.Instance != null)
        {
            playerHealth.TakeDamage(10);
            AudioManager.instance.PlaySfxSalah();
            QuizManager2.Instance.AnswerWrong();
            StartCoroutine(HandleQuizsalah(QuizManager2.Instance));
        }
        else
        {
            Debug.LogWarning("QuizManager2.Instance is null. Quizsalah2() cannot proceed.");
        }
    }

    private IEnumerator HandleQuizsalah(object quizManager)
    {
        if (quizManager is QuizManager1 quizManager1)
        {
            StartCoroutine(quizManager1.jawabanS(1.0f));
        }

        if (quizManager is QuizManager2 quizManager2)
        {
            StartCoroutine(quizManager2.jawabanS(1.0f));
        }

        yield return new WaitForSeconds(1.5f); // Tambahkan sedikit waktu untuk memastikan coroutine selesai
        Destroy(gameObject);
        player.SetActive(true);
        quiz.SetActive(false);
        Debug.Log("jawaban salah");
    }
}
