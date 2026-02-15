using System.Collections.Generic;
using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float m_MoveSpeed = 5;
    public float m_DeadZone = -5;

    public List<Sprite> m_PipeSprites = new List<Sprite>();

    private Bird bird;

    private void Start()
    {
        // Find and assign a reference to the Bird instance
        bird = FindAnyObjectByType<Bird>();

        UpdatePipeSprite();
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < m_DeadZone)
            Destroy(gameObject);

        // Move the pipe only if the bird is alive
        if (bird.m_IsDead == false)
            transform.position += -(transform.right * GetMoveSpeed) * Time.deltaTime;
    }

    private void UpdatePipeSprite()
    {
        // Get all child  (Top pipe and Bottom pipe) and update sprite
        foreach (var sp in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            // Ignore any coin
            if (sp.name == "Coin") continue;
            sp.sprite = m_PipeSprites[GameManager.Instance.m_LevelIndex];
        }
    }

    public float GetMoveSpeed
    {
        get
        {
            float moveSpeed = 0;
            switch (GameManager.Instance.m_GameMode)
            {
                case GameMode.Easy:
                    moveSpeed = 1;
                    break;
                case GameMode.Hard:
                    moveSpeed = 3;
                    break;
                case GameMode.Mission:
                    moveSpeed = 2;
                    break;
            }

            return moveSpeed;
        }
    }
}
