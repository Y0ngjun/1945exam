using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public GameObject enemyBullet;
    public GameObject[] items;

    private Rigidbody2D rg2D;
    private float fireDelay;
    private bool onDead;
    private float time;
    private float moveSpeed;
    private GameObject player;

    void Start()
    {
        onDead = false;
        time = 0.0f;
        fireDelay = 0.0f;
        moveSpeed = Random.Range(4.0f, 7.0f);
        player = GameObject.FindGameObjectWithTag("Player");
        rg2D = GetComponent<Rigidbody2D>();
        Move();
    }

    void Update()
    {
        if (onDead)
        {
            time += Time.deltaTime;
        }
        if (time > 0.6f)
        {
            Destroy(gameObject);
            if(gameObject.CompareTag("itemDropEnemy"))
            {
                int temp = Random.Range(0,2);
                Instantiate(items[temp], transform.position, Quaternion.identity);
            }
        }
        FireBullet();
    }

    void Move()
    {
        if (player == null)
        {
            return;
        }

        Vector3 distance = player.transform.position - transform.position;
        Vector3 dir = distance.normalized;
        rg2D.linearVelocity = dir * moveSpeed;
    }

    void FireBullet()
    {
        if (player == null)
        {
            return;
        }

        fireDelay += Time.deltaTime;
        if (fireDelay > 3.0f)
        {
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            fireDelay -= 3.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            animator.SetInteger("State", 1);
            OnDead();
        }
        if(collision.CompareTag("BlockCollider"))
        {
            OnDisappear();
        }
    }

    private void OnDead()
    {
        onDead = true;
    }

    private void OnDisappear()
    {
        Destroy(gameObject);
    }
}
