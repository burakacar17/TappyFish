using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstackleSpawner : MonoBehaviour
{
    public GameObject obstackle;
    public float maxTime;
    float timer;
    public float maxY;
    public float minY;
    float randomY;
    
    void Start()
    {
        //InstantiateObstackle();
    }

    
    void Update()
    {
        if (GameManager.gameOver == false && GameManager.gameStarted == true)
        {
            timer += Time.deltaTime;
            if (timer >= maxTime)
            {
                randomY = Random.Range(minY, maxY);
                InstantiateObstackle();
                timer = 0;
            }
        }

        
        
    }

    public void InstantiateObstackle()
    {
        GameObject newObstackle = Instantiate(obstackle);
        newObstackle.transform.position = new Vector2(transform.position.x, randomY);
    }
}
