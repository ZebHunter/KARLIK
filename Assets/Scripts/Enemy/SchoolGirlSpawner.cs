using UnityEngine;
using UnityEngine.UIElements;

public class SchoolGirlSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject SchoolGirl;
    public static bool AllowSpawning {get; set;}
    public static bool GirlSpawned {get; set;}

    void Start()
    {
        AllowSpawning = false;
        GirlSpawned = false;
    }

    void Update()
    {
        if(AllowSpawning && !GirlSpawned && Timer.Instance.GameTime % 30  == 0){
            var randomPoint = new Vector2(Random.Range(0,EnemySpawner.Instance.CameraWidth * 1.01f), Random.Range(0, EnemySpawner.Instance.CameraWidth*1.01f));
            Instantiate(SchoolGirl, new Vector3(randomPoint.x, randomPoint.y, 0), Quaternion.identity);
            GirlSpawned = true;
        }
    }
}