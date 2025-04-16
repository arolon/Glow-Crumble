using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class IngredientManager : MonoBehaviour
{
    public TMP_Text bowlText;
    public TMP_Text availabilityText;
    public GameObject bakeButton;
    
   
    // public string loseSceneName = "LoseScene";
    
    private string currentIngredients = "";
    private Dictionary<string, int> ingredientClickCount = new Dictionary<string, int>();

    public List<string> requiredIngredients = new List<string> { "egg", "butter", "flour" };
    
    private HashSet<string> collectedIngredients = new HashSet<string>();

    private HashSet<string> validIngredients = new HashSet<string> { "egg", "butter", "flour" };

    void Start()
    {
        
        if (bakeButton != null)
        {
            bakeButton.SetActive(false);
        }
        
        
        if (availabilityText != null)
        {
            availabilityText.gameObject.SetActive(true);
            availabilityText.text = "Select all required ingredients before baking!";
        }
    }

    public void AddIngredient(string ingredientName)
    {
        
        if (!validIngredients.Contains(ingredientName))
        {
            bowlText.text = "Incorrect ingredient!";
            bakeButton.SetActive(false); 
            // Invoke("LoadLoseScene", 2f);
            return;
        }

        if (ingredientClickCount.ContainsKey(ingredientName))
        {
            ingredientClickCount[ingredientName]++;
        }
        else
        {
            ingredientClickCount[ingredientName] = 1;
        }


        collectedIngredients.Add(ingredientName);

        
        if (ingredientClickCount[ingredientName] > 3)
        {
            RemoveIngredient(ingredientName);
        }
        else
        {
            
            if (string.IsNullOrEmpty(currentIngredients))
            {
                currentIngredients = ingredientName;
            }
            else
            {
                currentIngredients += ", " + ingredientName;
            }

            UpdateBowl();
        }
        
        CheckBakeButtonStatus();
    }

    private void RemoveIngredient(string ingredientName)
    {
        currentIngredients = currentIngredients.Replace(ingredientName, "").Trim();
        
        currentIngredients = currentIngredients.Replace(", ,", ",").TrimStart(',').TrimEnd(',');
        UpdateBowl();
        bowlText.text = "You are out of " + ingredientName + "!";

        
        collectedIngredients.Remove(ingredientName);

        
        GameObject ingredientObject = GameObject.Find(ingredientName);
        if (ingredientObject != null)
        {
            Destroy(ingredientObject);
        }
        
        CheckBakeButtonStatus();
    }

    private void UpdateBowl()
    {
        bowlText.text = "Added: " + currentIngredients;
    }
    
    private void CheckBakeButtonStatus()
    {
        bool allIngredientsCollected = true;
        
        foreach (string ingredient in requiredIngredients)
        {
            if (!collectedIngredients.Contains(ingredient))
            {
                allIngredientsCollected = false;
                break;
            }
        }
        
        if (bakeButton != null)
        {
            bakeButton.SetActive(allIngredientsCollected);
        }
        
        if (!allIngredientsCollected)
        {
            if (availabilityText != null)
            {
                availabilityText.gameObject.SetActive(true);
                availabilityText.text = "You need to grab all ingredients before baking!";
            }
        }
        else
        {
            if (availabilityText != null)
            {
                availabilityText.gameObject.SetActive(false);
            }
        }
    }
    
    // private void LoadLoseScene()
    // {
    //     SceneManager.LoadScene(loseSceneName);
    // }
    
    public void AttemptToBake()
    {
        if (collectedIngredients.Count < requiredIngredients.Count)
        {
            if (availabilityText != null)
            {
                availabilityText.gameObject.SetActive(true);
                availabilityText.text = "You need to grab all ingredients before baking!";
            }
            return;
        }
        
        foreach (string ingredient in collectedIngredients)
        {
            if (!requiredIngredients.Contains(ingredient))
            {
                bowlText.text = "Incorrect ingredients in the recipe!";
                // Invoke("LoadLoseScene", 2f);
                return;
            }
        }
    
    }
}