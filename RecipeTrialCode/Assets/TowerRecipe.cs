using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TowerRecipe : MonoBehaviour
{
    public Recipe[] towerRecipeList;
    public List<RecipeTracker> RecipeTrackerList;

    private void Awake()
    {
        SetupTempTwrRecipeTrackerList();
       // PrintRecipeTracker();
    }
    public void SetupTempTwrRecipeTrackerList()
    {
        RecipeTrackerList = new List<RecipeTracker>();
        foreach(var recipe in towerRecipeList)
        {
            //Copy towerRecipeList to tempTwrRecipeTrackerList
            Dictionary<string, string> tempTowerReq = new Dictionary<string, string>(); 
            foreach (var twr in recipe.towerRequired)
            {
                tempTowerReq.Add(twr, null);
            }
            RecipeTracker tempRecipe = new RecipeTracker(recipe.towerOutput, tempTowerReq);
            RecipeTrackerList.Add(tempRecipe);
        }
        

    }
    public void UpdateRecipeTracker(List<string> towerBuilt)
    {
        foreach(var recipe in RecipeTrackerList) //For every recipe check if the tower built matches 
        {
            foreach (var e_towerBuilt in towerBuilt) //For every tower in towerBuilt do:
            {
                //Get towerOutput that matches
                string towerOutput = null;
                //Checks if twr is in the recipe
                foreach (var twr in recipe.towerRequired)
                {
                    if (e_towerBuilt == twr.Key)
                        towerOutput = recipe.towerOutput;
                }

                //Insert into dictionary if theres tower required
                if (towerOutput != null)
                    InsertRecipeTrackerList(towerOutput, e_towerBuilt);
            }
        }
        CheckForUpgrades();
    }
    public string CheckIfTwrMatches(string towerRequiredName)
    {
        bool towerIsThere = false;
        string towerOutput = null;
        //Checks each recipe
        foreach (var recipe in towerRecipeList)
        {
            towerIsThere = recipe.towerRequired.Contains(towerRequiredName);
            if (towerIsThere)
                towerOutput = recipe.towerOutput;
        }
        return towerOutput;
    }

    private void CheckForUpgrades()
    {
        //Replace with GO if wan to ping GO
        List<RecipeTracker> UpgRecipeList = new List<RecipeTracker>();
        foreach(var recipe in RecipeTrackerList)
        {
            if (CheckIfCanUpgrade(recipe)) //Recipe full
            {
                UpgRecipeList.Add(recipe);
            }
        }

        //Ping event - Put foreach to ping each individual obj
        if (UpgRecipeList.Count != 0)
        {
            foreach(var recipe in UpgRecipeList)
            {
                Debug.Log("======UPGRADE : " + recipe.towerOutput);
            }
        }
    }

    private bool CheckIfCanUpgrade(RecipeTracker recipe)
    {
        bool AllTowerBuilt = true;
        foreach(var twr in recipe.towerRequired)
        {
            if (twr.Value == null)
                AllTowerBuilt = false;
        }
        return AllTowerBuilt;
    }

    private void InsertRecipeTrackerList(string twrOutput, string twrBuilt)
    {
        //bool towerIsThere = false;
        foreach (var recipe in RecipeTrackerList)
        {
            if (recipe.towerOutput == twrOutput)
            {
                recipe.towerRequired[twrBuilt] = twrBuilt;
            }
        }
    }

    
    public void PrintRecipeTracker()
    {
        foreach (var recipe in RecipeTrackerList)
        {
            Debug.Log("----------------------------------");
            Debug.Log("=====[ "+ recipe.towerOutput + " ]=====");
            foreach(var twr in recipe.towerRequired)
            {
                Debug.Log(twr.Key + " : " + twr.Value);
            }
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrintRecipeTracker();
        }
    }
}
[System.Serializable]
public class Recipe
{
    public string towerOutput;
    public List<string> towerRequired;
}
public class RecipeTracker
{
    public string towerOutput;
    public Dictionary<string, string> towerRequired;

    public RecipeTracker(string twrOutput, Dictionary<string, string> twrRequired)
    {
        towerOutput = twrOutput;
        towerRequired= twrRequired;
    }
}
/*
// //Check if it is in any tower recipe
foreach (var towerRecipe in towerRecipeList) //For every recipe in recipeList do:
{
    string towerRequired =
                if (towerRequired != null) //Contained 
    {
        Debug.Log(e_towerBuilt + ": " + towerRecipe.towerOutput);

        //If tower output exist
        foreach (var possibleTowerRecipe in possibleTowerOutputList)
        {
            //If tower output already exist
            if (possibleTowerRecipe.towerOutput == towerRecipe.towerOutput)
            {
                //Check if there are duplicates
                foreach (var possibleTwrOutput in possibleTowerRecipe.towerRequired)
                {
                    if (possibleTwrOutput == towerRequired)
                    {
                        //Do nothing
                    }
                    else
                    {
                    }
                }
            }
        }
    }
}*/