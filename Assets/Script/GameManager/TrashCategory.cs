using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trash Category", menuName = "Trash Category")]
public class TrashCategory : ScriptableObject
{
    public string categoryName;
    public GameObject trashPrefab;
}
