using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRecipeManager : MonoBehaviour
{
    public TowerRecipe twrRecipeDB;
    public List<string> towersBuilt;
    void Start()
    {
        towersBuilt.Add("Chipped Emerald");
        towersBuilt.Add("Chipped Topaz");
        towersBuilt.Add("Chipped Sapphire");
        towersBuilt.Add("Item4");
        towersBuilt.Add("Item5");

        //Check if item is in the recipe

        twrRecipeDB.UpdateRecipeTracker(towersBuilt);

    }
    
}
