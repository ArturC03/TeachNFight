using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    [SerializeField] private float time;

    private float lifetime;
    [SerializeField] private int damage;
    public GameObject pai;
    private Rigidbody2D rb;
    [SerializeField] private GameObject sprite;
    [SerializeField] private int rot;

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        sprite.transform.Rotate(0, 0, rot * Time.deltaTime);

        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > time)
        {
            transform.position = new Vector3(0, -1000, 0);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            gameObject.SetActive(false);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hurtbox" && pai.transform != collision.transform.root)
        {
            hit = true;
            boxCollider.enabled = false;

            Debug.Log("levou" + damage);
            if (transform.position.x > collision.transform.position.x)
                collision.transform.root.GetComponent<PlayerHealth>().TakeDamage(damage, -150f);
            else
                collision.transform.root.GetComponent<PlayerHealth>().TakeDamage(damage, 150f);
            transform.position = new Vector3(0, -1000, 0);
            transform.rotation = new Quaternion(0,0,0,0);
            gameObject.SetActive(false);



        }
    }
    public void SetDirection(float _direction)
    {
        gameObject.SetActive(true);

        lifetime = 0;
        direction = _direction;
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }


    }
