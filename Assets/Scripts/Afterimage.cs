using UnityEngine;

public class Afterimage : MonoBehaviour
{
    float fadeSpeed = 0.005f;
    SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer.color.a <= fadeSpeed) {
            Destroy(this.gameObject);
            return;
        }

        Color newColor = new Color(spriteRenderer.color.r, spriteRenderer.color.b, spriteRenderer.color.g, spriteRenderer.color.a - fadeSpeed);
        spriteRenderer.color = newColor;
    }
}
