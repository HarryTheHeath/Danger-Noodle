using System;
using UnityEngine;

public class Food : MonoBehaviour
{
    private FoodSpawner foodSpawner;
    
    private void Awake()
    {
        foodSpawner = GameObject.Find("Food Spawner").GetComponent<FoodSpawner>();
    }
    
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Body>())
        {
            transform.position = foodSpawner.GetFoodSpawnPosition();
        }
        var head = col.GetComponent<Head>();
        if (head == null)
        {
            return;
        }
        head.Eat(this);
    }
}
