using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionwithStructure : MonoBehaviour, IInteract
{
    public virtual void InteractWithStructure()
    {
        Debug.Log(name);
    }
}
