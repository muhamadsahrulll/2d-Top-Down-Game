using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class mainmenu : MonoBehaviour
{

    public TextMeshProUGUI level1ScoreText;
    public TextMeshProUGUI level2ScoreText;
    public TextMeshProUGUI level3ScoreText;
    public TextMeshProUGUI level4ScoreText;
    public TextMeshProUGUI totalOrganicScoreText;
    public GameObject tidakcukup;


    private void Update()
    {
        // Update text for each level score
        level1ScoreText.text = "Score: " + ScoreManager.Instance.level1OrganicScore.ToString();
        level2ScoreText.text = "Score: " + ScoreManager.Instance.level2OrganicScore.ToString();
        level3ScoreText.text = "Score: " + ScoreManager.Instance.level3OrganicScore.ToString();
        level4ScoreText.text = "Score: " + ScoreManager.Instance.level4OrganicScore.ToString();

        // Update text for total organic score
        totalOrganicScoreText.text = "Total Score: " + ScoreManager.Instance.totalOrganicScore.ToString();
    }
    public void KeluarGame()
    {
        Application.Quit();
    }

    public void loadscene (string scene)
    {
        // Memeriksa apakah level dapat diakses berdasarkan skor pemain
        if (CanAccessLevel(scene))
        {
            SceneManager.LoadScene(scene);
        }
        else
        {
            StartCoroutine(levelwarning(3f));
            Debug.Log("Level belum bisa diakses!");
        }
    }

    // Fungsi untuk memeriksa apakah level dapat diakses berdasarkan skor pemain
    private bool CanAccessLevel(string scene)
    {
        int requiredScore = 100; // Skor yang dibutuhkan untuk membuka setiap level selanjutnya

        // Memeriksa level yang akan dimuat
        switch (scene)
        {
            case "level1":
                // Pengecekan skor untuk membuka level 1
                return ScoreManager.Instance.totalOrganicScore >= 0;
            case "level2":
                // Pengecekan skor untuk membuka level 2
                return ScoreManager.Instance.totalOrganicScore >= requiredScore;
            case "level3":
                // Pengecekan skor untuk membuka level 3
                return ScoreManager.Instance.totalOrganicScore >= 2 * requiredScore;
            case "level4":
                // Pengecekan skor untuk membuka level 4
                return ScoreManager.Instance.totalOrganicScore >= 3 * requiredScore;
            default:
                Debug.LogWarning("Nama level tidak valid.");
                return false;
        }
    }

    IEnumerator levelwarning(float seconds)
    {
        tidakcukup.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        tidakcukup.gameObject.SetActive(false);
    }
}
