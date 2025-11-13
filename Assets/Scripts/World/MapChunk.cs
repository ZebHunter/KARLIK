using UnityEngine;
// using LayingItem;
public class MapChunk
{
    public int Size;
    private float ObstacleProp;
    private float CryptProp;
    private float GroundProp;
    private float ItemProp;
    private float HealProp;

    public GameObject[,] Tiles;

    public MapChunk(int size, float obstacleProp, float cryptProp, float itemProp, float healProp)
    {
        Size = size;
        ObstacleProp = obstacleProp;
        GroundProp = 1 - ObstacleProp;
        CryptProp = cryptProp*obstacleProp;
        ItemProp = itemProp*GroundProp;
        HealProp = healProp = ItemProp*healProp;
        Tiles = new GameObject[size, size];
    }

    public void Generate(GameObject[] terrainPrefabs, GameObject[] stonePrefabs, GameObject CryptPrefab, GameObject[] itemPrefubs, int X, int Y, int WorldSize)
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Vector3 terrainPos = new Vector3(
                    (X - WorldSize / 2) * Size + i,
                    (Y - WorldSize / 2) * Size + j,
                    10
                );
                Vector3 obstaclePos = new Vector3(
                    (X - WorldSize / 2) * Size + i,
                    (Y - WorldSize / 2) * Size + j,
                    0.5f
                );


                float prop = Random.Range(.0f, 1.0f);
                TileType type;
                if (prop <= ObstacleProp)
                    if (prop <= CryptProp) type = TileType.Crypt;
                    else type = TileType.Stone;
                else
                {
                    type = TileType.Ground;
                    prop -= ObstacleProp;
                    if (prop <= GroundProp*ItemProp)
                    {
                        LayingTypes itemType = (prop <= HealProp) ? LayingTypes.Healing : LayingTypes.Damage_ups;
                        UnityEngine.Object.Instantiate(itemPrefubs[(int)itemType], obstaclePos, Quaternion.identity);
                    }
                }

                UnityEngine.Object.Instantiate(terrainPrefabs[Random.Range(0, terrainPrefabs.Length)], terrainPos, Quaternion.identity);
                switch(type)
                {
                    case TileType.Crypt:
                        Tiles[i, j] = UnityEngine.Object.Instantiate(CryptPrefab, obstaclePos, Quaternion.identity);
                        break;
                    case TileType.Stone:
                        Tiles[i, j] = UnityEngine.Object.Instantiate(stonePrefabs[Random.Range(0, stonePrefabs.Length)], obstaclePos, Quaternion.identity);
                        break;
                }
                
            }
        }
    }
}
