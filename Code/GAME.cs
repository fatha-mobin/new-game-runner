using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 2f;
    private float nextSpawn = 0f;

    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        float spawnYPosition = Random.Range(-3f, 3f); // Adjust according to your game design
        Instantiate(obstaclePrefab, new Vector2(transform.position.x, spawnYPosition), Quaternion.identity);
    }
}
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public float backgroundSpeed = 0.1f;
    private Vector2 offset;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        offset = new Vector2(backgroundSpeed, 0);
    }

    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    private int score;

    void Start()
    {
        score = 0;
        UpdateScore();
    }

    void Update()
    {
        // Update score based on time or distance
        score = (int)Time.timeSinceLevelLoad;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isGravityFlipped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.G)) // G to flip gravity
        {
            FlipGravity();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void FlipGravity()
    {
        isGravityFlipped = !isGravityFlipped;
        rb.gravityScale *= -1;
        transform.Rotate(0, 0, 180f);
    }
}
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 2f;
    private float nextSpawn = 0f;

    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        float spawnYPosition = Random.Range(-3f, 3f); // Adjust according to your game design
        Instantiate(obstaclePrefab, new Vector2(transform.position.x, spawnYPosition), Quaternion.identity);
    }
}
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public float backgroundSpeed = 0.1f;
    private Vector2 offset;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        offset = new Vector2(backgroundSpeed, 0);
    }

    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    private int score;

    void Start()
    {
        score = 0;
        UpdateScore();
    }

    void Update()
    {
        // Update score based on time or distance
        score = (int)Time.timeSinceLevelLoad;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostDuration = 5f;
    public float speedMultiplier = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Boost(other));
        }
    }

    private IEnumerator Boost(Collider2D player)
    {
        PlayerController controller = player.GetComponent<PlayerController>();
        controller.speed *= speedMultiplier;
        yield return new WaitForSeconds(boostDuration);
        controller.speed /= speedMultiplier;
        Destroy(gameObject);
    }
}
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothing = 5f;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}