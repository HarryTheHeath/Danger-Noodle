using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Head : Body
{
    private MoveDirection moveDirection;
    public Body bodyPrefab;
    private Food foodPrefab;
    public Food avokado;
    public Food avokado2;
    public Food sushi;
    public Food sushi2;
    private int lastRandom;

    public enum MoveDirection
    {
        Up, // = 0
        Left, // = 1
        Down, // = 2
        Right, // = 3
        Count // = 4
    }
    Quaternion GetRotation()
    {
        return moveDirection switch
        {
            MoveDirection.Up => Quaternion.Euler(0, 0, 0),
            MoveDirection.Down => Quaternion.Euler(0, 0, 180),
            MoveDirection.Left => Quaternion.Euler(0, 0, 90),
            MoveDirection.Right => Quaternion.Euler(0, 0, -90),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    Vector3 GetMovement()
    {
        return moveDirection switch
        {
            MoveDirection.Up => Vector3.up,
            MoveDirection.Down => Vector3.down,
            MoveDirection.Left => Vector3.left,
            MoveDirection.Right => Vector3.right,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private bool canTurn = true;

    IEnumerator Start()
    {
        transform.position = Vector3.zero;
        SpawnFood();
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            MoveTo(transform.position + GetMovement(), GetRotation());
            canTurn = true;
        }
    }
    
    Vector3 GetFoodSpawnPosition()
    {
        return new Vector3(
            Random.Range(-3, 4),
            Random.Range(-3, 4)
        );
    }

    private void SpawnFood()
    {
        var random = Random.Range(0, 3);

        if (random == lastRandom)
        {
            random = (lastRandom + 2);
            if (random > 3)
            {
                if (random == 4)
                {
                    random = 0;
                }
                else
                {
                    random = 1;
                }
            }
        }
        switch (random)
        {
            case 0:
                foodPrefab = avokado;
                Debug.Log("Spawn Avocado");
                lastRandom = 0;
                break;
            case 1:
                foodPrefab = avokado2;
                Debug.Log("Spawn Happy Avocado");
                lastRandom = 1;
                break;
            case 2:
                foodPrefab = sushi;
                Debug.Log("Spawn Salmon Sushi");
                lastRandom = 2;
                break;
            case 3:
                foodPrefab = sushi2;
                Debug.Log("Spawn Avocado Sushi");
                lastRandom = 3;
                break;
        }
        Instantiate(foodPrefab, GetFoodSpawnPosition(), Quaternion.identity);
    }

    void Update()
    {
        if (!canTurn)
            return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            moveDirection = (MoveDirection)(((int)moveDirection + 1 + (int)MoveDirection.Count) % (int)MoveDirection.Count);
            canTurn = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveDirection = (MoveDirection)(((int)moveDirection - 1 + (int)MoveDirection.Count) % (int)MoveDirection.Count);
            canTurn = false;
        }
    }

    public void Eat(Food food)
    {
        Destroy(food.gameObject);
        SpawnFood();
        
        Grow(bodyPrefab);
    }
}
