using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private FlashWhite flashWhite;
    private ObjectPooler destroyEffectPool;

    private int lives;
    private int maxLives = 5;
    private int damage = 1;
    private int ExperiencetoGive = 1;

    [SerializeField] private Sprite[] sprites;
    float pushX;
    float pushY;

    void OnEnable() {
        lives = maxLives;
        transform.rotation = Quaternion.identity;
        pushX = Random.Range(-1f,0);
        pushY = Random.Range(-1f,1f);
        if (rb) rb.linearVelocity = new Vector2(pushX,pushY);
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        pushX = Random.Range(-1f,0);
        pushY = Random.Range(-1f,1f);
        if (rb) rb.linearVelocity = new Vector2(pushX,pushY);
        flashWhite = GetComponent<FlashWhite>();
        destroyEffectPool = GameObject.Find("Boom2Pool").GetComponent<ObjectPooler>();

        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        float randomScale = Random.Range(0.6f, 1f);
        transform.localScale = new Vector2(randomScale, randomScale);
        lives = maxLives;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player) player.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage, bool giveExperience){
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.hitRock);
        lives -= damage;
        if (lives > 0) {
            flashWhite.Flash();
        } else if (lives <= 0){
            GameObject destroyEffect = destroyEffectPool.GetPooledObject();
            destroyEffect.transform.position = transform.position;
            destroyEffect.transform.rotation = transform.rotation;
            destroyEffect.transform.localScale = transform.localScale;
            destroyEffect.SetActive(true);
            AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.boom2);
            flashWhite.Reset();
            gameObject.SetActive(false);
            if (giveExperience == true) {
                PlayerController.Instance.GetExperience(ExperiencetoGive);
            }
        }
    }
}
