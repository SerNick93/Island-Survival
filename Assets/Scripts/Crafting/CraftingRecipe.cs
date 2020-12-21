using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crafting Recipies", menuName = "ScriptableObjects/Crafting Recipies/Crafting Recipies")]
public class CraftingRecipe : ScriptableObject
{
    [SerializeField]
    public string recipeName;
    [SerializeField]
    public List<Item> inputItems = new List<Item>();
    [SerializeField]
    public List<Item> outputItems = new List<Item>();
    [SerializeField]
    public List<Item> toolsRequired = new List<Item>();
}
