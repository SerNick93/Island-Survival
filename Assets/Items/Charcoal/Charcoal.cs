using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charcoal", menuName = "ScriptableObjects/Items/Charcoal")]
public class charcoal : Item
{
    public override void AddToActions()
    {
        ItemActionMethods.Clear();
        ItemActionMethods.Add(Use);
        base.AddToActions();
    }
    public override void Use()
    {
        base.Use();
        Debug.Log(ItemName + "Is being used.");
    }
}
