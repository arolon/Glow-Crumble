using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private bool nearCookingStation = false;
    public GameObject craftingUI;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0)
        {
            animator.SetBool("isWalking", true);
            spriteRenderer.flipX = (movement.x > 0) ? true : false;
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Handle directional animations
        if (movement.y < 0)
        {
            animator.SetBool("isMovingUp", true);
            animator.SetBool("isMovingDown", false);
        }
        else if (movement.y > 0)
        {
            animator.SetBool("isMovingUp", false);
            animator.SetBool("isMovingDown", true);
        }
        else
        {
            animator.SetBool("isMovingUp", false);
            animator.SetBool("isMovingDown", false);
        }
        

        if (nearCookingStation && Input.GetKeyDown(KeyCode.E))
        {
            //ToggleCraftingUI();
            Debug.Log("CRAFTING");
            SceneManager.LoadScene("PlayScene");
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement.normalized * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "CookingStation") // Check GameObject name
        {
            nearCookingStation = true;
            Debug.Log("CLOSE CRAFTING");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "CookingStation") // Check GameObject name
        {
            nearCookingStation = false;
            /*craftingUI.SetActive(false);*/ // Close UI when leaving the station
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Exit") // Ensure only the player triggers it
        {
            SceneManager.LoadScene("Map");
        }
    }

    //void ToggleCraftingUI()
    //{
    //    craftingUI.SetActive(!craftingUI.activeSelf);
    //}
}
