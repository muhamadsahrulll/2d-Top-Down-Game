using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InorganicManager : MonoBehaviour
{
    public static InorganicManager Instance;

    // Variabel untuk sampah anorganik
    public int inorganicTrashCollected = 0;
    public int totalInorganicTrash = 5;
    public int inorganicTrashReward = 10;

    public GameObject tutor1;

    public TextMeshProUGUI inorganicTrashCollectedText;
    public TextMeshProUGUI totalInorganicTrashText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadGameData();
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
            // Berikan hadiah ke pemain
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

}
