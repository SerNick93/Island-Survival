using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopulateBuildingsInterface : MonoBehaviour
{
    public BuildingSO[] recipies;
    public GameObject recipeListing;
    public Transform recipeListingParent;

    // Start is called before the first frame update
    void Start()
    {
        foreach (BuildingSO buildings in recipies)
        {
            GameObject listingInstance = Instantiate(recipeListing, recipeListingParent);
            listingInstance.GetComponent<TextMeshProUGUI>().text = buildings.buildingName;
            recipeListing.GetComponent<BuildingListing>().InitRecipe(buildings);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
