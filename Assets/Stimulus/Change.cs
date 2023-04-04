using UnityEngine;

public class Change : MonoBehaviour
{
    public Sprite newSprite;  // The new sprite to use

    void OnEnable()
    {
        // Get the SpriteRenderer component on this object
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Set the sprite to the new sprite
            spriteRenderer.sprite = newSprite;
        }
        else
        {
            Debug.LogWarning("No SpriteRenderer component found on this object!");
        }
    }

    void OnValidate()
    {
        // If this script is attached to an enabled object in the editor, 
        // update the sprite in the editor preview window
        if (gameObject.activeInHierarchy)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                Debug.LogWarning("No SpriteRenderer component found on this object!");
            }
        }
    }
}
