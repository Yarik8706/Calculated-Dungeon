using UnityEngine;

public class RandomSpriteControl : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private bool flipToX = true;
    [SerializeField] private bool flipToY = true;

    private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Length > 1)
        {
            _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }
        if (flipToX && Random.Range(0, 2) == 0)
        {
            _spriteRenderer.flipX = true;
        }
        if (flipToY && Random.Range(0, 2) == 0)
        {
            _spriteRenderer.flipY = true;
        }
    }
}