using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BowlMovement : MonoBehaviour
{
    public Button bakeButton; // Reference to the Bake button
    public float moveSpeed = 5f; // Speed at which the bowl moves
    private bool isMoving = false;
    public TMP_Text messageText;
    public BreadSpawnScript breadSpawnScript;
    void Start()
    {
        // Ensure the Bake button triggers the MoveBowlDown method when clicked
        bakeButton.onClick.AddListener(MoveBowlDown);
    }

    void Update()
    {
        // If the bowl is moving, update its position
        if (isMoving)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
    }

    void MoveBowlDown()
    {
        isMoving = true;
        // Stop the bowl after 5 seconds
        Invoke("StopBowl", 5f);
        messageText.text = "Baking in progress!";
        StartCoroutine(StopBowlAndSpawnBread());
    }

    void StopBowl()
    {
        isMoving = false;
    }
    private IEnumerator StopBowlAndSpawnBread()
    {
        yield return new WaitForSeconds(5f); // Wait for the bowl to stop moving
        isMoving = false;
        breadSpawnScript.StartBaking(); // Start the baking process to spawn bread after delay
    }
}
