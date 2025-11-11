using UnityEngine;

public class MapChunk
{
    public int Size;
    public float ObstacleProp;
    public float CryptProp;
    public GameObject[,] Tiles;

    public MapChunk(int size, float obstacleProp, float cryptProp)
    {
        Size = size;
        ObstacleProp = obstacleProp;
        CryptProp = cryptProp;
        Tiles = new GameObject[size, size];
    }

    public void Generate(GameObject[] terrainPrefabs, GameObject[] obstaclePrefabs, GameObject CryptPrefab, int X, int Y)
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                float prop = Random.Range(.0f, 1.0f);
                TileType type = prop <= ObstacleProp ? (prop <= ObstacleProp*CryptProp ? TileType.Crypt : TileType.Obstacle) : TileType.Ground;
                switch(type)
                {
                    case TileType.Crypt:
                        Tiles[i, j] = UnityEngine.Object.Instantiate(CryptPrefab, new Vector3(X * Size - 80 + i, Y * Size - 80 + j, 0), Quaternion.identity);
                        break;
                    case TileType.Ground:
                        Tiles[i, j] = UnityEngine.Object.Instantiate(terrainPrefabs[Random.Range(0, terrainPrefabs.Length)], new Vector3(X * Size - 80 + i, Y * Size - 80 + j, 0), Quaternion.identity);
                        break;
                    case TileType.Obstacle:
                        Tiles[i, j] = UnityEngine.Object.Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], new Vector3(X * Size - 80 + i, Y * Size - 80 + j, 0), Quaternion.identity);
                        break;
                }
                
            }
        }
    }
}
