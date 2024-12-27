using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    private GameManager gm;
    private bool isGameOver = false;

    private void Start()
    {
        // Cache the GameManager reference at the start
        gm = Object.FindFirstObjectByType<GameManager>();
    }

    private void OnEnable()
    {
        GameManager.GameOverEvent += OnGameOver; // Subscribe to Game Over event
    }

    private void OnDisable()
    {
        GameManager.GameOverEvent -= OnGameOver; // Unsubscribe to avoid memory leaks
    }

    void Update()
    {
        if (!isGameOver)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, vertical, 0);
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGameOver) return;

        if (collision.CompareTag("Collectible"))
        {
            if (gm != null)
            {
                gm.AddScore(10); // Add 10 points for each collectible
            }
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            if (gm != null)
            {
                gm.LoseLife(); // Reduce lives
            }
        }
    }

    private void OnGameOver()
    {
        isGameOver = true;
    }
}
