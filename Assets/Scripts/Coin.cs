using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager _gameManager;

    public AudioClip coin;
    private AudioSource _audioSource;

    public SpriteRenderer renderSprite;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        renderSprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

       if(collision.gameObject.CompareTag("Player"))  //destruccion de monedas y activar el contador de moneda
       {
            renderSprite.enabled = false;
            _audioSource.PlayOneShot(coin);
            Destroy(gameObject, 1.2f);
       }
    }
}