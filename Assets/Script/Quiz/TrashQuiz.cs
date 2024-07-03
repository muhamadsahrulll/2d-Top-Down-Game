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
        QuizManager1.Instance.CollectQuizTrash();
        AudioManager.instance.PlaySfxBenar();
        StartCoroutine(HandleQuizbenar1());
    }

    private IEnumerator HandleQuizbenar1()
    {
        if (QuizManager1.Instance != null)
        {
            StartCoroutine(QuizManager1.Instance.jawabanB(1.0f));
        }

        if (QuizManager2.Instance != null)
        {
            StartCoroutine(QuizManager2.Instance.jawabanB(1.0f));
        }

        yield return new WaitForSeconds(1.5f); // Tambahkan sedikit waktu untuk memastikan coroutine selesai
        Destroy(gameObject);
        player.SetActive(true);
        quiz.SetActive(false);

        Debug.Log("jawaban benar");
    }

    public void Quizbenar2()
    {
        QuizManager2.Instance.CollectQuizTrash2();
        AudioManager.instance.PlaySfxBenar();
        StartCoroutine(HandleQuizbenar2());
    }

    private IEnumerator HandleQuizbenar2()
    {
        if (QuizManager1.Instance != null)
        {
            StartCoroutine(QuizManager1.Instance.jawabanB(1.0f));
        }

        if (QuizManager2.Instance != null)
        {
            StartCoroutine(QuizManager2.Instance.jawabanB(1.0f));
        }

        yield return new WaitForSeconds(1.5f); // Tambahkan sedikit waktu untuk memastikan coroutine selesai
        Destroy(gameObject);
        player.SetActive(true);
        quiz.SetActive(false);
        Debug.Log("jawaban benar");
    }

    public void Quizsalah1()
    {
        playerHealth.TakeDamage(10);
        AudioManager.instance.PlaySfxSalah();
        if (QuizManager1.Instance != null)
        {
            QuizManager1.Instance.AnswerWrong();
            StartCoroutine(HandleQuizsalah1());
        }
        else
        {
            Debug.LogWarning("QuizManager1.Instance is null. Quizsalah1() cannot proceed.");
        }
    }

    private IEnumerator HandleQuizsalah1()
    {
        if (QuizManager1.Instance != null)
        {
            StartCoroutine(QuizManager1.Instance.jawabanS(1.0f));
        }

        if (QuizManager2.Instance != null)
        {
            StartCoroutine(QuizManager2.Instance.jawabanS(1.0f));
        }

        yield return new WaitForSeconds(1.5f); // Tambahkan sedikit waktu untuk memastikan coroutine selesai
        Destroy(gameObject);
        player.SetActive(true);
        quiz.SetActive(false);
        Debug.Log("jawaban salah");
    }

    public void Quizsalah2()
    {
        playerHealth.TakeDamage(10);
        AudioManager.instance.PlaySfxSalah();
        if (QuizManager2.Instance != null)
        {
            QuizManager2.Instance.AnswerWrong();
            StartCoroutine(HandleQuizsalah2());
        }
        else
        {
            Debug.LogWarning("QuizManager2.Instance is null. Quizsalah2() cannot proceed.");
        }
    }

    private IEnumerator HandleQuizsalah2()
    {
        if (QuizManager1.Instance != null)
        {
            StartCoroutine(QuizManager1.Instance.jawabanS(1.0f));
        }

        if (QuizManager2.Instance != null)
        {
            StartCoroutine(QuizManager2.Instance.jawabanS(1.0f));
        }

        yield return new WaitForSeconds(1.5f); // Tambahkan sedikit waktu untuk memastikan coroutine selesai
        Destroy(gameObject);
        player.SetActive(true);
        quiz.SetActive(false);
        Debug.Log("jawaban salah");
    }
}
