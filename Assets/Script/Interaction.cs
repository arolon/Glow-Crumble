using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour
{
    public string npcName;
    public string recipeName;
    public List<string> npcDialogue;
    public string requiredItem;
    public GameObject interactionPromptPrefab; 
    public GameObject notificationPrefab;
    private GameObject interactionPromptInstance;
    private GameObject notificationInstance;
    private bool playerNearby = false;
    private int dialogueIndex = 0;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithNPC();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            ShowInteractionPrompt();
            Debug.Log($"Player is near {npcName}.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            HideInteractionPrompt();
        }
    }

    void ShowInteractionPrompt()
    {
        if (interactionPromptInstance == null)
        {
            interactionPromptInstance = Instantiate(interactionPromptPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            interactionPromptInstance.transform.SetParent(transform);
        }
    }

    void HideInteractionPrompt()
    {
        if (interactionPromptInstance != null)
        {
            Destroy(interactionPromptInstance);
        }
    }

    void InteractWithNPC()
    {
        if (dialogueIndex < npcDialogue.Count)
        {
            ShowNotification(npcDialogue[dialogueIndex]);
            dialogueIndex++;
        }
        else
        {
            if (!string.IsNullOrEmpty(requiredItem) && !GameManager.Instance.Inventory.Contains(requiredItem))
            {
                ShowNotification($"Bring me a {requiredItem} first!");
            }
            else
            {
                LearnRecipe();
            }
        }
    }

    void LearnRecipe()
    {
        if (!GameManager.Instance.LearnedRecipes.Contains(recipeName))
        {
            GameManager.Instance.LearnedRecipes.Add(recipeName);
            ShowNotification($"You learned a new recipe: {recipeName}!");
        }
        else
        {
            ShowNotification("You already know this recipe!");
        }
    }

    void ShowNotification(string message)
    {
        if (notificationInstance != null)
        {
            Destroy(notificationInstance);
        }

        notificationInstance = Instantiate(notificationPrefab, Vector3.zero, Quaternion.identity);
        notificationInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        notificationInstance.GetComponentInChildren<TextMeshProUGUI>().text = message;
        interactionPromptInstance.gameObject.SetActive(false);
        Invoke("HideNotification", 2f);
    }

    void HideNotification()
    {
        if (notificationInstance != null)
        {
            Destroy(notificationInstance);
            interactionPromptInstance.gameObject.SetActive(true);
        }
    }
}
