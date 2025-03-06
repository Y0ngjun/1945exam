using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [Header("References")]
    public Transform[] enemySpawners;
    public GameObject[] enemyGameObjects;

    private float time;
    private float respawnTime;
    private int enemyCount;
    private int[] randomCount;
    private int wave;
    private GameObject player;

    void Start()
    {
        time = 0;
        respawnTime = 4.0f;
        enemyCount = 5;
        randomCount = new int[enemyCount];
        wave = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Timer();
    }

    void Timer()
    {
        time += Time.deltaTime;
        if (time > respawnTime)
        {
            RandomPos();
            EnemyCreate();
            wave++;
            time -= respawnTime;
        }
    }

    void RandomPos()
    {
        for (int i = 0; i < enemyCount; ++i)
        {
            randomCount[i] = Random.Range(0, enemySpawners.Length);
            Debug.Log(randomCount[i]);
        }
    }

    void EnemyCreate()
    {
        if(player==null)
        {
            return;
        }

        for(int i=0;i<enemyCount; ++i)
        {
            int tmpCnt = Random.Range(0,enemyGameObjects.Length);
            GameObject tmp = GameObject.Instantiate(enemyGameObjects[tmpCnt]);
            tmp.transform.position = enemySpawners[randomCount[i]].position;
            float tmpX = tmp.transform.position.x;
            float result = Random.Range(tmpX - 1.5f, tmpX + 1.5f);
            tmp.transform.position = new Vector3(result, tmp.transform.position.y, transform.position.z);
        }
    }
}
