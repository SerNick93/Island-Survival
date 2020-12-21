using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopulateCraftingInterface : MonoBehaviour
{
    // Start is called before the first frame update

    public CraftingRecipe[] recipies;
    public GameObject recipeListing;
    public Transform recipeListingParent;

    void Start()
    {
        foreach (CraftingRecipe cr in recipies)
        {
           GameObject listingInstance = Instantiate(recipeListing, recipeListingParent);
            listingInstance.GetComponent<TextMeshProUGUI>().text = cr.recipeName;
            recipeListing.GetComponent<CraftingListing>().InitRecipe(cr);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
