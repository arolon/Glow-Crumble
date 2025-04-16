using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BreadSpawnScript : MonoBehaviour
{
    public GameObject breadPrefab;
    public Transform spawnPosition;
    public Transform tablePosition;
    public float moveSpeed = 2f;
    public TMP_Text messageText;
    public string winSceneName = "WinScene";
    public Button serveButton;
    //public AudioClip audioClip;

    private bool breadIsSpawned = false;

    void Start()
    {
        serveButton.gameObject.SetActive(false);
        if (breadPrefab == null || spawnPosition == null || tablePosition == null)
        {
            Debug.LogError("Please assign all references in the Inspector.");
            return;
        }
    }

    public void StartBaking()
    {
        if (breadIsSpawned) return; 

        StartCoroutine(SpawnBreadAfterDelay(2f));
    }

    private IEnumerator SpawnBreadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject bread = Instantiate(breadPrefab, spawnPosition.position, Quaternion.identity);
        breadIsSpawned = true;

        while (Vector3.Distance(bread.transform.position, tablePosition.position) > 0.1f)
        {
            bread.transform.position = Vector3.MoveTowards(bread.transform.position, tablePosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        bread.transform.position = tablePosition.position;
        messageText.text = "Your bread is ready!";
        serveButton.gameObject.SetActive(true);
        //ServeOrder();
        //yield return new WaitForSeconds(2f);

        //SceneManager.LoadScene(winSceneName);
    }
    public void ServeOrder()
    {
        
        
        SceneManager.LoadScene(winSceneName);
    }
}
