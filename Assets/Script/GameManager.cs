using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int organicTrashCollected = 0;
    public int totalOrganicTrash = 5;
    public int organicTrashReward = 10;

    public TextMeshProUGUI trashCollectedText;
    public TextMeshProUGUI totalOrganicTrashText;


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
        UpdateUIText();
    }

    private void Update()
    {
        UpdateUIText();
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
            // Berikan hadiah ke pemain
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
}
