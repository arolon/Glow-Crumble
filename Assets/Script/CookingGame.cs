using UnityEngine;
using UnityEngine.UI; 
using System.Collections;
using TMPro;

public class CookingGame : MonoBehaviour
{
    public GameObject bowl; 
    public GameObject spoon;
    private bool ingredient1Added = false;
    private bool ingredient2Added = false;
    public TMP_Text messageText;
    public TMP_Text mixText;

    public float moveSpeed = 5f;

    public void MixIngredients()
    {
        Debug.Log("Mixing ingredients!");
        StartCoroutine(MoveSpoonToBowl());
    }


    private Vector3 initialSpoonPosition; 

    private IEnumerator MoveSpoonToBowl()
    {
        initialSpoonPosition = spoon.transform.position;

        Vector3 center = bowl.transform.position;
        float radius = 0.5f;
        float speed = 2f;
        float duration = 2f; 
        float elapsedTime = 0f;


        
        while (elapsedTime < duration)
        {
            float angle = elapsedTime * speed * Mathf.PI * 2;

            
            float x = center.x + Mathf.Cos(angle) * radius + 0.5f; 

            
            float y = center.y + Mathf.Sin(angle) * radius + 1f;

            spoon.transform.position = new Vector3(x, y, spoon.transform.position.z);

            elapsedTime += Time.deltaTime;
            yield return null;
            messageText.text = "Mixing ingredients!";
        }

        Debug.Log("Mixing completed!");

       
        float returnSpeed = 2f;
        float returnDuration = 1f; 

        elapsedTime = 0f;
        while (elapsedTime < returnDuration)
        {
            spoon.transform.position = Vector3.Lerp(spoon.transform.position, initialSpoonPosition, elapsedTime / returnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

      
        spoon.transform.position = initialSpoonPosition;
        messageText.text = "Ready to bake!";
        Destroy(spoon);
        mixText.text = " ";

    }

    



   
}
