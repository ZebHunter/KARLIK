using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI timer;
    public static Timer Instance {get; private  set;}
    public float lifeTime;
    private float gameTime;
    public float GameTime => lifeTime;
    public Timer()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Update()
    {
        timer.text = lifeTime.ToString();
        gameTime += 1 * Time.deltaTime;
        if (gameTime >= 1)
        {
            if (lifeTime > 0)
            {
                lifeTime -= 1;
            }
            else
            {
                if (EventBus.Instance != null)
                {
                    EventBus.Instance.Invoke(new EndSignal());
                }
                SceneManager.LoadScene(3);
            }

            gameTime -= 1;
            if (lifeTime % 15 == 0)
            {
                EnemySpawner.Instance.AddMaxEnemies(1);
            }
        }
        if (lifeTime == 10)
        {
            timer.color = Color.yellow;
        }
        if (lifeTime == 3)
        {
            timer.color = Color.green;
        }

    }
}
