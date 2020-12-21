using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceNodeInteraction : MonoBehaviour
{
    [SerializeField]
    ResourceNode thisNode;
    [SerializeField]
    float thisNodesHealth;
    ItemPickup itemOutputs;
    float dropQuantity;

    private void Start()
    {
        thisNodesHealth = thisNode.nodeBaseHealth + 5;
        dropQuantity = UnityEngine.Random.Range(1f, 5f);
    }
    public void HarvestThisNode()
    {
        Debug.Log("Item being Harvested.");
        DamageNode();
    }

    private void DamageNode()
    {
        if (thisNodesHealth > 0)
        {
            thisNodesHealth = thisNodesHealth - 5f;
        }
        else
        {
            Destroy(gameObject);
            for (int i = 0; i < thisNode.outputItems.Length; i++)
            {
                for (int j = 0; j < (int)dropQuantity; j++)
                {
                    InventoryController.MyInstance.AddItem(thisNode.outputItems[i]);
                }
                //itemOutputs.AddItemToInventory(thisNode.outputItems[i]);
            }
        }
        
    }
}
