using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData InstanceData;
    public bool onSound = true;

    private void Awake()
    {
        if (InstanceData == null)
        {
            InstanceData = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
