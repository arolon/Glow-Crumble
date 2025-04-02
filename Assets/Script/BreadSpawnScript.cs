using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BreadSpawnScript : MonoBehaviour
{
    public GameObject breadPrefab;  // The bread prefab to spawn
    public Transform spawnPosition; // Position where the bread will appear (above the bowl)
    public Transform tablePosition; // Position where the bread will be placed (on the table)
    public float moveSpeed = 2f;    // Speed at which the bread moves
    public TMP_Text messageText;

    private bool breadIsSpawned = false; // Flag to prevent multiple spawnings

    void Start()
    {
        // Ensure the breadPrefab is linked in the Inspector
        if (breadPrefab == null || spawnPosition == null || tablePosition == null)
        {
            Debug.LogError("Please assign all references in the Inspector.");
            return;
        }
    }

    public void StartBaking()
    {
        if (breadIsSpawned) return;  // Prevent spawning if the bread is already on the scene

        StartCoroutine(SpawnBreadAfterDelay(2f));  // Wait for 10 seconds
    }

    private IEnumerator SpawnBreadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        // Instantiate the bread at the spawn position
        GameObject bread = Instantiate(breadPrefab, spawnPosition.position, Quaternion.identity);
        breadIsSpawned = true;

        // Move the bread to the table position
        while (Vector3.Distance(bread.transform.position, tablePosition.position) > 0.1f)
        {
            bread.transform.position = Vector3.MoveTowards(bread.transform.position, tablePosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        bread.transform.position = tablePosition.position;  // Ensure it reaches the table position
        messageText.text = "Your bread is ready!";
    }
}
