using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [Header("Ingredient Settings")]
    [SerializeField] private GameObject[] ingredientPrefabs; 
    [SerializeField] private int numberToSpawn = 5;         
    [SerializeField] private Vector2 spawnAreaMin;          
    [SerializeField] private Vector2 spawnAreaMax;          

    void Start()
    {
        SpawnIngredients();
    }

    void SpawnIngredients()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
           
            int randomIndex = Random.Range(0, ingredientPrefabs.Length);
            GameObject ingredient = ingredientPrefabs[randomIndex];

           
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPosition = new Vector2(x, y);

           
            Instantiate(ingredient, spawnPosition, Quaternion.identity);
        }
    }
}
