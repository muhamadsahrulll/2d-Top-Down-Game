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
    public TextMeshProUGUI level5ScoreText;
    public TextMeshProUGUI level6ScoreText;
    public TextMeshProUGUI totalOrganicScoreText;
    public GameObject tidakcukup;

    [Header("HISTORI")]
    public TextMeshProUGUI level1ScoreTxt;
    public TextMeshProUGUI level2ScoreTxt;
    public TextMeshProUGUI level3ScoreTxt;
    public TextMeshProUGUI level4ScoreTxt;
    public TextMeshProUGUI level5ScoreTxt;
    public TextMeshProUGUI level6ScoreTxt;


    private void Update()
    {
        // Update text for each level score
        level1ScoreText.text = "Score: " + ScoreManager.Instance.level1OrganicScore.ToString();
        level2ScoreText.text = "Score: " + ScoreManager.Instance.level2OrganicScore.ToString();
        level3ScoreText.text = "Score: " + ScoreManager.Instance.level3OrganicScore.ToString();
        level4ScoreText.text = "Score: " + ScoreManager.Instance.level4OrganicScore.ToString();
        level5ScoreText.text = "Score: " + ScoreManager.Instance.level5OrganicScore.ToString();
        level6ScoreText.text = "Score: " + ScoreManager.Instance.level6OrganicScore.ToString();

        // Update text for total organic score
        totalOrganicScoreText.text = "Total Score: " + ScoreManager.Instance.totalOrganicScore.ToString();

        //update score histori
        level1ScoreTxt.text = "Score Level 1 kamu " + ScoreManager.Instance.level1OrganicScore.ToString();
        level2ScoreTxt.text = "Score Level 2 kamu " + ScoreManager.Instance.level2OrganicScore.ToString();
        level3ScoreTxt.text = "Score Level 3 kamu " + ScoreManager.Instance.level3OrganicScore.ToString();
        level4ScoreTxt.text = "Score Level 4 kamu " + ScoreManager.Instance.level4OrganicScore.ToString();
        level5ScoreTxt.text = "Score Level 5 kamu " + ScoreManager.Instance.level5OrganicScore.ToString();
        level6ScoreTxt.text = "Score Level 6 kamu " + ScoreManager.Instance.level6OrganicScore.ToString();
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
                return ScoreManager.Instance.totalOrganicScore >= 0;
            case "level2":
                return ScoreManager.Instance.totalOrganicScore >= requiredScore;
            case "level3":
                return ScoreManager.Instance.totalOrganicScore >= 2 * requiredScore;
            case "level4":
                return ScoreManager.Instance.totalOrganicScore >= 275;
            case "level5":
                return ScoreManager.Instance.totalOrganicScore >= 350; // Tambahkan pengecekan untuk level 5
            case "level6":
                return ScoreManager.Instance.totalOrganicScore >= 350;
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
