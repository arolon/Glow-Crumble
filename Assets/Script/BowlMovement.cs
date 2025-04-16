using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BowlMovement : MonoBehaviour
{
    public Button bakeButton;
    public float moveSpeed = 5f;
    private bool isMoving = false;
    public TMP_Text messageText;
    public BreadSpawnScript breadSpawnScript;
    void Start()
    {
        bakeButton.onClick.AddListener(MoveBowlDown);
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
    }

    void MoveBowlDown()
    {
        isMoving = true;
        
        Invoke("StopBowl", 1f);
        messageText.text = "Baking in progress!";
        StartCoroutine(StopBowlAndSpawnBread());
    }

    void StopBowl()
    {
        isMoving = false;
    }
    private IEnumerator StopBowlAndSpawnBread()
    {
        yield return new WaitForSeconds(1f);
        isMoving = false;
        breadSpawnScript.StartBaking(); 
    }
}
