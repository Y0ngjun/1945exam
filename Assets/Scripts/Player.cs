using UnityEngine;
public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float speed;

    [Header("References")]
    public Collider2D col;
    public GameObject bullet;
    public Animator animator;

    private float time;
    private float fireDelay;
    private bool onDead;
    private Vector3 limitMax;
    private Vector3 limitMin;
    private Vector3 temp;
 
    void Start()
    {
        time = 0.0f;
        fireDelay = 0.0f;
        onDead = false;
        Vector3 size = col.bounds.size;
        limitMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - Camera.main.transform.position.z)) + size / 2;
        limitMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - Camera.main.transform.position.z)) - size / 2;
    }

    void Update()
    {
        Move();
        FireBullet();
        OnDeadCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("enemyBullet"))
        {
            animator.SetInteger("State", 1);
            onDead=true;
        }
    }

    private void OnDeadCheck()
    {
        if(onDead)
        {
            time += Time.deltaTime;
        }
        if(time > 0.6f)
        {
            Destroy(gameObject);
        }
    }

    public void FireBullet()
    {
        fireDelay += Time.deltaTime;
        if (fireDelay > 0.3f)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            fireDelay -= 0.3f;
        }
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(new Vector3(x, y, 0));

        if (transform.position.x > limitMax.x)
        {
            temp.x = limitMax.x;
            temp.y = transform.position.y;
            transform.position = temp;
        }
        if (transform.position.y > limitMax.y)
        {
            temp.y = limitMax.y;
            temp.x = transform.position.x;
            transform.position = temp;
        }
        if (transform.position.x < limitMin.x)
        {
            temp.x = limitMin.x;
            temp.y = transform.position.y;
            transform.position = temp;
        }
        if (transform.position.y < limitMin.y)
        {
            temp.y = limitMin.y;
            temp.x = transform.position.x;
            transform.position = temp;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(limitMin, new Vector2(limitMax.x, limitMin.y));
        Gizmos.DrawLine(limitMin, new Vector2(limitMin.x, limitMax.y));
        Gizmos.DrawLine(limitMax, new Vector2(limitMax.x, limitMin.y));
        Gizmos.DrawLine(limitMax, new Vector2(limitMin.x, limitMax.y));
    }
}