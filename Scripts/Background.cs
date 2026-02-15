using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public List<Sprite> m_Backgrounds = new();
    private SpriteRenderer backgroundSpriteRenderer;

    private void Start()
    {
        backgroundSpriteRenderer = GetComponent<SpriteRenderer>();
        // Change background
        backgroundSpriteRenderer.sprite = m_Backgrounds[GameManager.Instance.m_LevelIndex];
    }
}
