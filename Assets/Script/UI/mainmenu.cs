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
        SceneManager.LoadScene(scene);
    }
}
