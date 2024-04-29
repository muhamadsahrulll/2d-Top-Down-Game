using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InorganicManager : MonoBehaviour
{
    public static InorganicManager Instance;
    public Timer timer;

    // Variabel untuk sampah anorganik
    public int inorganicTrashCollected = 0;
    public int totalInorganicTrash = 5;
    public int inorganicTrashReward = 10;

    public GameObject tutor1;
    public GameObject selamat;
    public GameObject player;

    public TextMeshProUGUI inorganicTrashCollectedText;
    public TextMeshProUGUI totalInorganicTrashText;

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

    public void CollectInorganicTrash()
    {
        inorganicTrashCollected++;
        PlayerPrefs.SetInt("InorganicTrashCollected", inorganicTrashCollected);
        UpdateUIText();
        if (inorganicTrashCollected >= totalInorganicTrash)
        {
            Debug.Log("Inorganic Trash Mission Completed");
            PlayerPrefs.SetInt("InorganicTrashReward", inorganicTrashReward);
            selamat.SetActive(true);
            timer.PauseTimer();
        }
    }

    public void LoadGameData()
    {
        inorganicTrashCollected = PlayerPrefs.GetInt("InorganicTrashCollected", 0);
        UpdateUIText();
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("InorganicTrashCollected", inorganicTrashCollected);
    }

    private void UpdateUIText()
    {
        inorganicTrashCollectedText.text = "Inorganic Trash Collected: " + inorganicTrashCollected + "/" + totalInorganicTrash;
        totalInorganicTrashText.text = "Total Inorganic Trash: " + totalInorganicTrash;
    }

    private void RestartGame()
    {
        // Reset nilai-nilai yang perlu di-reset
        inorganicTrashCollected = 0;
        PlayerPrefs.SetInt("InorganicTrashCollected", inorganicTrashCollected);

        // Restart timer
        FindObjectOfType<Timer>().timeRemaining = 60f;

        // Restart scene atau lakukan langkah-langkah lain yang diperlukan untuk memulai ulang game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player.SetActive(true);
    }

    public void Restartgame()
    {
        RestartGame();
    }

}
