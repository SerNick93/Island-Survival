using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    [SerializeField]
    public BuildingSO thisBuilding;
    MeshRenderer mr;
    BoxCollider bc;
    InteractionwithStructure interaction;
    

    public void Start()
    {
        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();
        interaction = GetComponent<InteractionwithStructure>();
    }
    public void BuildObject()
    {
        StartCoroutine(StartBuild());
    }
    public IEnumerator StartBuild()
    {
        Debug.Log("StartingBuild");
        yield return new  WaitForSeconds(thisBuilding.completionTime);
        mr.material.color = Color.white;
        bc.isTrigger = false;

        if (interaction != null)
        {
            interaction.enabled = true;
            interaction.InteractWithStructure();
        }

        Debug.Log("BuildFinished");
        this.enabled = false;
    }
}
