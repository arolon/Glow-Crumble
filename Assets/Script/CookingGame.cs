using UnityEngine;
using UnityEngine.UI; // Import this for the Button component
using System.Collections; // Add this to use IEnumerator and coroutines
using TMPro;

public class CookingGame : MonoBehaviour
{
    public GameObject bowl;   // Reference to the bowl object
    public GameObject spoon;  // Reference to the spoon (which is now a button)
    private bool ingredient1Added = false;
    private bool ingredient2Added = false;
    public TMP_Text messageText;
    public TMP_Text mixText;

    // The speed of the spoon movement
    public float moveSpeed = 5f;

    // This function will be called when the spoon (acting as the button) is clicked
    public void MixIngredients()
    {
        Debug.Log("Mixing ingredients!");
        StartCoroutine(MoveSpoonToBowl());
    }


    private Vector3 initialSpoonPosition; // To store the initial position of the spoon

    private IEnumerator MoveSpoonToBowl()
    {
        initialSpoonPosition = spoon.transform.position; // Store the initial position of the spoon

        Vector3 center = bowl.transform.position; // Center of mixing
        float radius = 0.5f; // Adjust based on the desired mixing range
        float speed = 2f; // Adjust for how fast the spoon moves
        float duration = 2f; // How long the mixing should last
        float elapsedTime = 0f;


        // Mixing animation
        while (elapsedTime < duration)
        {
            float angle = elapsedTime * speed * Mathf.PI * 2; // Full circular motion
            float x = center.x + Mathf.Cos(angle) * radius;
            float y = center.y + Mathf.Sin(angle) * radius;

            spoon.transform.position = new Vector3(x, y, spoon.transform.position.z);

            elapsedTime += Time.deltaTime;
            yield return null;
            messageText.text = "Mixing ingredients!";
        }

        Debug.Log("Mixing completed!");

        // Move the spoon back to its initial position after mixing
        float returnSpeed = 2f; // Adjust the return speed
        float returnDuration = 1f; // Adjust how fast the spoon returns

        elapsedTime = 0f;
        while (elapsedTime < returnDuration)
        {
            spoon.transform.position = Vector3.Lerp(spoon.transform.position, initialSpoonPosition, elapsedTime / returnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the spoon is exactly at the initial position at the end
        spoon.transform.position = initialSpoonPosition;
        messageText.text = "Ready to bake!";
        Destroy(spoon);
        mixText.text = " ";

    }

    //private IEnumerator MoveSpoonToBowl()
    //{
    //    Vector3 center = bowl.transform.position; // Center of mixing
    //    Vector3 originalBowlPosition = bowl.transform.position; // Store original bowl position
    //    float radius = 0.5f; // Adjust based on the desired mixing range
    //    float speed = 2f; // Adjust for how fast the spoon moves
    //    float duration = 2f; // How long the mixing should last
    //    float shakeIntensity = 0.10f; // Bowl shaking intensity
    //    float elapsedTime = 0f;

    //    while (elapsedTime < duration)
    //    {
    //        // Circular motion for spoon
    //        float angle = elapsedTime * speed * Mathf.PI * 2; // Full circular motion
    //        float x = center.x + Mathf.Cos(angle) * radius;
    //        float y = center.y + Mathf.Sin(angle) * radius;

    //        spoon.transform.position = new Vector3(x, y, spoon.transform.position.z);

    //        // Shake the bowl slightly
    //        bowl.transform.position = originalBowlPosition + new Vector3(
    //            Random.Range(-shakeIntensity, shakeIntensity),
    //            Random.Range(-shakeIntensity, shakeIntensity),
    //            0f
    //        );

    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    // Reset bowl position after shaking
    //    bowl.transform.position = originalBowlPosition;
    //    Debug.Log("Mixing completed!");
    //}




   
}
