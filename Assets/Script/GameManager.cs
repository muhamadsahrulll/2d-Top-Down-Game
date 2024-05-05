using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Timer timer;

    public int organicTrashCollected = 0;
    public int totalOrganicTrash = 5;
    public int organicTrashReward = 10;

    

    public TextMeshProUGUI trashCollectedText;
    public TextMeshProUGUI totalOrganicTrashText;
    public GameObject tutor1;
    public GameObject selamat;
    public GameObject player;
    

    



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadGameData();
        UpdateUIText();
        tutor1.SetActive(true);
        
    }

    private void Update()
    {
        UpdateUIText();
        SaveGameData();
    }

    public void CollectOrganicTrash()
    {
        organicTrashCollected++;
        PlayerPrefs.SetInt("OrganicTrashCollected", organicTrashCollected);
        UpdateUIText();

        if (organicTrashCollected >= totalOrganicTrash)
        {
            Debug.Log("Misi Selesai");
            PlayerPrefs.SetInt("OrganicTrashReward", organicTrashReward);
            selamat.SetActive(true);
            timer.PauseTimer();
        }
    }

    

   

    public void LoadGameData()
    {
        organicTrashCollected = PlayerPrefs.GetInt("OrganicTrashCollected", 0);
        
        UpdateUIText();
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("OrganicTrashCollected", organicTrashCollected);
    }

    private void UpdateUIText()
    {
        trashCollectedText.text = "Sampah Terkumpul: " + organicTrashCollected + "/" + totalOrganicTrash;
        totalOrganicTrashText.text = "Total Sampah Organik: " + totalOrganicTrash;
    }

    private void RestartGame()
    {
        // Reset nilai-nilai yang perlu di-reset
        organicTrashCollected = 0;
        PlayerPrefs.SetInt("OrganicTrashCollected", organicTrashCollected);

        // Restart timer
        FindObjectOfType<Timer>().timeRemaining = 60f;

        // Restart scene atau lakukan langkah-langkah lain yang diperlukan untuk memulai ulang game
        SceneManager.LoadScene("level1");
        player.SetActive(true);
        
    }

    public void Restartgame()
    {
        RestartGame();
    }


}
