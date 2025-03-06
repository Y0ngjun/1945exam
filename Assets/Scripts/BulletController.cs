using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Settings")]
    public float speed;
    
    private float time;

    void Start()
    {
        time = 0.0f;
    }

    void Update()
    {
        FireBullet();
        DestroyBullet();
    }

    void FireBullet()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        time += Time.deltaTime;
        if (time > 2.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }
}
