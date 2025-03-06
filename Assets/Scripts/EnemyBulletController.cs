using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    public float rotateSpeed;

    [Header("References")]
    public Rigidbody2D rg2D;
    
    private GameObject player;
    private float time;

    void Start()
    {
        time = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        FireBullet();
    }

    void Update()
    {
        RotateBullet();
        DestroyBullet();
    }

    private void FireBullet()
    {
        Vector3 distance = player.transform.position - transform.position;
        Vector3 dir = distance.normalized;
        rg2D.linearVelocity = dir * moveSpeed;
    }

    private void RotateBullet()
    {
        transform.rotation = Quaternion.Euler(0, 0, time * rotateSpeed);
    }

    private void DestroyBullet()
    {
        time +=Time.deltaTime;
        if(time >5.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
