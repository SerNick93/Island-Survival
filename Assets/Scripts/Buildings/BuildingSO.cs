using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSO : ScriptableObject
{
    public string buildingName;
    public string buildingDescription;
    public float completionTime;
    public List<Item> inputItems;
    public GameObject structurePrefab;

    public void InitRecipe()
    {

    }    
}
