using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnPrefab;

    private float spawnInterval = 2f;
    private float spawnTimer = 0f ;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer >= spawnInterval)
        {
            SpawnObj();
            spawnTimer = 0f;
        }
        spawnTimer = Time.deltaTime;
    }

    public void SpawnObj()
    {
        PlayerController player = GameObject.FindAnyObjectByType<PlayerController>();
        float playerZ = player.transform.position.z;

        Vector3 randomPos = Vector3.zero;
        randomPos.x = Random.Range(-8, 8);
        randomPos.z = playerZ + 100;
        object obj = Instantiate(spawnPrefab, this.transform);
    }
}
