using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Invader : MonoBehaviour
{
    private SpriteRenderer _SpriteRenderer;
    private int animationFrame;

    public GameObject hit_effect;
    public Sprite[] animations;
    public float animationTime = 1.0f;
    public System.Action killed;
    public System.Action enemyHitBoundary;

    private void Awake()
    {
        _SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Animation), this.animationTime, this.animationTime);
    }

    void Animation()
    {
        animationFrame++;

        if (animationFrame >= animations.Length) animationFrame = 0;

        _SpriteRenderer.sprite = animations[animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "laser")
        {
            Instantiate(hit_effect, gameObject.transform.position - new Vector3(0, -0.3f, 0), Quaternion.identity);
            gameObject.SetActive(false);
            killed.Invoke();
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "boundary") enemyHitBoundary.Invoke();
    } 
}
