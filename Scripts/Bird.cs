using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb;
    public float m_FlapForce;
    public bool m_IsDead = false;

    // Index used to play the dead bird animation when the bird dies
    private const int DEAD_INDEX = 3;

    private Animator birdAnimator;
    // Hit and coin audio clips
    public AudioSource m_HitAudio, m_CoinAudio;

    [HideInInspector] public bool m_IsReadyToPlay = false;
    public bool IsReadyAndAlive
    {
        get
        {
            return m_IsReadyToPlay == true && m_IsDead == false;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        birdAnimator = GetComponent<Animator>();

        // Disable physics interactions by setting the Rigidbody to Kinematic
        rb.bodyType = RigidbodyType2D.Kinematic;

        // Play correct animation
        birdAnimator.SetInteger("Bird_Index", (int)GameManager.Instance.m_FlappyBirdType);
    }

    void Update()
    {
        // Is the bird die ? return
        if (m_IsDead) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            //If the player presses the key, we indicate that the player is ready.
            if (m_IsReadyToPlay == false)
            {
                // Enable full physics interactions by setting the Rigidbody to Dynamic
                rb.bodyType = RigidbodyType2D.Dynamic;
                m_IsReadyToPlay = true;
                GameUI.Instance.HideTapToStartUI();
            }
            // else
            rb.linearVelocity = Vector2.up * m_FlapForce;
        }
    }
    private void Die()
    {
        // Play hit sound
        m_HitAudio.Play();
        // Mark is dead
        m_IsDead = true;

        // Play death animation
        birdAnimator.SetInteger("Bird_Index", DEAD_INDEX);
        // Show game over panel
        FindAnyObjectByType<GameOverPanelUI>(FindObjectsInactive.Include).ShowGameOverPanel();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // The bird is already die
        if (m_IsDead) return;

        Die();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Don't count anything if the bird is dead
        if (m_IsDead) return;

        if (other.CompareTag("Pipe"))
        {
            GameUI.Instance.AddScore();
        }
        else if (other.CompareTag("Coin"))
        {
            // Play the coin sound
            m_CoinAudio.Play();
            GameUI.Instance.AddCoin();
            // Destroy coin
            Destroy(other.gameObject);
        }
    }

}
