using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charcoal Dust", menuName = "ScriptableObjects/Items/Charcoal Dust")]
public class CharcoalDust : Item
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
