using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [Header("World Settings")]
    public int worldSize = 10;
    public int chunkSize = 16;

    [SerializeField]
    private float obstacleProp;

    [SerializeField]
    private float cryptProp;

    [SerializeField]
    private float itemProp;

    [SerializeField]
    private float healProp;

    [Header("Prefabs")]
    public GameObject[] terrainPrefabs;
    public GameObject[] stonePrefabs;
    public GameObject cryptPrefab;
    public GameObject[] itemPrefubs;

    private MapChunk[,] chunks;

    void Start()
    {
        GenerateWorld();
        GenerateBorders();
    }

    void GenerateWorld()
    {
        chunks = new MapChunk[worldSize, worldSize];

        for (int x = 0; x < worldSize; x++)
        {
            for (int y = 0; y < worldSize; y++)
            {
                chunks[x, y] = new MapChunk(chunkSize, obstacleProp, cryptProp, itemProp, healProp);
                chunks[x, y].Generate(terrainPrefabs, stonePrefabs, cryptPrefab, itemPrefubs, x, y, worldSize);
            }
        }
    }

    void GenerateBorders()
    {
        int totalSize = worldSize * chunkSize;
        int offset = totalSize / 2;

        int borderThickness = 8;

        for (int x = -borderThickness; x < 0; ++x)
        {
            for (int y = -borderThickness; y < 0; ++y)
            {
                PlaceGroundBlock(x - offset, y - offset);
                PlaceGroundBlock(-x + offset - 1, y - offset);
                PlaceGroundBlock(x - offset, -y + offset - 1);
                PlaceGroundBlock(-x + offset - 1, -y + offset - 1);

            }

            for (int y = 0; y < totalSize; ++y)
            {
                PlaceGroundBlock(x - offset, y - offset);
                PlaceGroundBlock(-x + offset - 1, y - offset);

                PlaceGroundBlock(y - offset, x - offset);
                PlaceGroundBlock(y - offset, -x + offset - 1);
            }
        }

        PlaceBorderBlock(-offset - 1, -offset - 1);
        PlaceBorderBlock(-offset - 1, offset);
        PlaceBorderBlock(offset, -offset - 1);
        PlaceBorderBlock(offset, offset);

        for (int x = 0; x < totalSize; x++)
        {
            PlaceBorderBlock(x - offset, -offset - 1);
            PlaceBorderBlock(x - offset, offset);

            PlaceBorderBlock(-offset - 1, x - offset);                   
            PlaceBorderBlock(offset, x - offset);
        }
    }

    void PlaceBorderBlock(int x, int y)
    {
        if (stonePrefabs.Length == 0) return;

        GameObject prefab = stonePrefabs[Random.Range(0, stonePrefabs.Length)];
        Vector3 pos = new Vector3(x, y, 0.5f);

        Instantiate(prefab, pos, Quaternion.identity);
    }

    void PlaceGroundBlock(int x, int y)
    {
        if (terrainPrefabs.Length == 0) return;

        GameObject prefab = terrainPrefabs[Random.Range(0, terrainPrefabs.Length)];
        Vector3 pos = new Vector3(x, y, 10);

        Instantiate(prefab, pos, Quaternion.identity);
    }
}
