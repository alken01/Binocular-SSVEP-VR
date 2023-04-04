using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public float frequency; 

    private SpriteRenderer spriteRenderer;
    private int refreshRate = 90;
    private float elapsedTime;
    private float timeToSwitch;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite1;
        elapsedTime = 0f;
        timeToSwitch = 1 / ( 2* frequency);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= timeToSwitch){
            elapsedTime = 0f;
            if (spriteRenderer.sprite == sprite1){
                spriteRenderer.sprite = sprite2;
            }
            else{
                spriteRenderer.sprite = sprite1;
            }
        }
    }

    public void SetSprites(Sprite sprite1, Sprite sprite2)
    {
        this.sprite1 = sprite1;
        this.sprite2 = sprite2;
        spriteRenderer.sprite = sprite1;
    }

    public void SetFrequency(float frequency)
    {
        this.frequency = frequency;
        timeToSwitch = 1 /( 2* frequency);
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
                spriteRenderer.sprite = sprite1;
            }
            else
            {
                Debug.LogWarning("No SpriteRenderer component found on this object!");
            }
        }
    }
}
