using UnityEngine;

public class grito : MonoBehaviour
{

    private bool hit;
    private float lifetime;
    [SerializeField] private int damage;
     public GameObject pai;

 private CircleCollider2D circleCollider;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        if (hit) return;
        lifetime += Time.deltaTime;
        if (lifetime > 0.5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.tag == "Hurtbox"  && pai.transform!=collision.transform.root)
        {
            
            hit = true;
            circleCollider.enabled = false;

            Debug.Log("levou" + damage);
            if (transform.position.x > collision.transform.position.x)
                collision.transform.root.GetComponent<PlayerHealth>().TakeDamage(damage, -100f);
            else
                collision.transform.root.GetComponent<PlayerHealth>().TakeDamage(damage, -00f);
            transform.position = new Vector3(0, -1000, 0);

        }
    }

    public void SetDirection(float _direction)
    {
        gameObject.SetActive(true);

        lifetime = 0;
        hit = false;
        circleCollider.enabled = true;

       

    }
}

