using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;

    public float lifeTime = 90f;
    private float gameTime;


    private void Update()
    {
        timer.text = "До победы осталось выжить: " + lifeTime + " сек";
        gameTime += 1 * Time.deltaTime;
        if (gameTime >= 1)
        {
            if (lifeTime > 0){
                lifeTime -= 1;
            } else {
                SceneSequence.LoadScene(3);
            }

            gameTime = 0;
        }
        if (lifeTime > 10)
        {
            timer.color = Color.red;
        }
        if (lifeTime <= 10)
        {
            timer.color = Color.yellow;
        }
        if (lifeTime <= 3)
        {
            timer.color = Color.green;
        }
        
    }
}
