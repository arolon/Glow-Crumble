using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BreadSpawner : MonoBehaviour
{
    public GameObject breadPrefab;
    public Vector3 targetPosition;
    public float moveSpeed = 3f;

    private GameObject breadInstance;
    private bool isMoving = false; 

    void Start()
    {
        Vector3 spawnPosition = new Vector3(-0.01f, -7.35f, -0.009410221f);
        breadInstance = Instantiate(breadPrefab, spawnPosition, Quaternion.identity);

        StartCoroutine(MoveBread());
    }

    IEnumerator MoveBread()
    {
        isMoving = true;

        while (Vector3.Distance(breadInstance.transform.position, targetPosition) > 0.1f)
        {
            breadInstance.transform.position = Vector3.MoveTowards(breadInstance.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;  // Wait until the next frame
        }

        // Stop the bread at the target position
        breadInstance.transform.position = targetPosition;
        isMoving = false;
    }
}
