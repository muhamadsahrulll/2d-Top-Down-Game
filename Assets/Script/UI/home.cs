using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class home : MonoBehaviour
{
    public void KeluarGame()
    {
        Application.Quit();
    }

    public void loadscene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
