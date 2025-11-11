using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [Header("World Settings")]
    public int worldSize = 10;
    public int chunkSize = 16;

    public float obstacleProp;

    [SerializeField]
    public float cryptProp;


    [Header("Prefabs")]
    public GameObject[] terrainPrefabs;
    public GameObject[] obstaclePrefabs;
    public GameObject cryptPrefab;
    // public GameObject flaskPrefab;

    private MapChunk[,] chunks;

    void Start()
    {
        GenerateWorld();
    }

    

    void GenerateWorld()
    {
        chunks = new MapChunk[worldSize, worldSize];

        for (int x = 0; x < worldSize; x++)
        {
            for (int y = 0; y < worldSize; y++)
            {
                chunks[x, y] = new MapChunk(chunkSize, obstacleProp, cryptProp);
                chunks[x, y].Generate(terrainPrefabs, obstaclePrefabs, cryptPrefab, x, y);
            }
        }

        // PlaceCrypts();
        // PlaceFlasks();
    }

    // void PlaceCrypts()
    // {
    //     int maxCrypts = worldSize * worldSize / 2;

    //     for (int i = 0; i < maxCrypts; i++)
    //     {
    //         int x = Random.Range(0, worldSize);
    //         int y = Random.Range(0, worldSize);
    //         Vector3 pos = new Vector3(x * chunkSize, 0, y * chunkSize);

    //         Instantiate(cryptPrefab, pos, Quaternion.identity);
    //     }
    // }

    // void PlaceFlasks()
    // {
    //     int count = Random.Range(10, 20);
    //     for (int i = 0; i < count; i++)
    //     {
    //         int x = Random.Range(0, worldSize);
    //         int y = Random.Range(0, worldSize);
    //         Vector3 pos = new Vector3(x * chunkSize + Random.Range(-4, 4), 0, y * chunkSize + Random.Range(-4, 4));
    //         Instantiate(flaskPrefab, pos, Quaternion.identity);
    //     }
    // }
}
