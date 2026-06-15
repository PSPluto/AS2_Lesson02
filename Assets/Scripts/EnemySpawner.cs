using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnPrefab;

    [SerializeField]private float spawnInterval = 2f;
    private float spawnTimer = 0f ;
    private PlayerController player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer >= spawnInterval)
        {
            SpawnObj();
            spawnTimer = 0f;
        }
        spawnTimer += Time.deltaTime;
    }

    public void SpawnObj()
    {
        float playerZ = player.transform.position.z;

        Vector3 randomPos = Vector3.zero;
        randomPos.x = Random.Range(-8, 8);
        randomPos.z = playerZ + 50;
        GameObject obj = Instantiate(spawnPrefab, randomPos, Quaternion.identity, this.transform);
    }
}
