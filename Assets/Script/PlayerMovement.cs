using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private bool nearCookingStation = false;
    private bool nearExit = false;
    
    public GameObject craftingUI;
    public InventorySystem inventory;
    
    
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;
    private float messageTimer = 0f;
    private bool showingMessage = false;

    public TMP_Text exitText;

    void Start()
    {
        // exitText.Text.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Hide message panel at start
        if (messagePanel != null)
            messagePanel.SetActive(false);
    }

    void Update()
    {
        HandleMovementInput();
        
        UpdateAnimations();
 
        HandleInteractions();

        UpdateMessageDisplay();
    }
    
    void HandleMovementInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }
    
    void UpdateAnimations()
    {

        animator.SetBool("isWalking", movement.x != 0);
        
        if (movement.x != 0)
            spriteRenderer.flipX = (movement.x > 0);
            

        animator.SetBool("isMovingUp", movement.y < 0);
        animator.SetBool("isMovingDown", movement.y > 0);
    }
    
    void HandleInteractions()
    {
       
        if (nearCookingStation && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Opening cooking interface");
            SceneManager.LoadScene("PlayScene");
        }
        
        if (nearExit && Input.GetKeyDown(KeyCode.E))
        {
            TryToExitLevel();
        }
    }
    
    void UpdateMessageDisplay()
    {
        if (showingMessage)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0)
            {
                HideMessage();
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement.normalized * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CookingStation")) 
        {
            nearCookingStation = true;
            ShowMessage("Press E to cook");
        }
        else if (other.gameObject.CompareTag("Exit"))
        {
            nearExit = true;
            ShowMessage("Press E to exit");
        }
        else if (other.gameObject.CompareTag("Ingredient"))
        {
        
            string itemName = other.gameObject.name;
            // Remove "(Clone)" suffix if it exists
            if (itemName.Contains("(Clone)"))
                itemName = itemName.Replace("(Clone)", "").Trim();
                
            inventory.AddItem(itemName);
            ShowMessage("Picked up: " + itemName);
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CookingStation"))
        {
            nearCookingStation = false;
        }
        else if (other.gameObject.CompareTag("Exit"))
        {
            nearExit = false;
        }
    }
    
    void TryToExitLevel()
    {
        if (inventory.HasAnyItems())
        {
            SceneManager.LoadScene("PlayScene");
        }
        else
        {
            ShowMessage("You can't go bake if you don't have ingredients!");
        }
    }
    
    void ShowMessage(string message)
    {
        if (messagePanel != null && messageText != null)
        {
            messagePanel.SetActive(true);
            messageText.text = message;
            messageTimer = 3.0f;
            showingMessage = true;
        }
    }
    
    void HideMessage()
    {
        if (messagePanel != null)
        {
            messagePanel.SetActive(false);
            showingMessage = false;
        }
    }
}