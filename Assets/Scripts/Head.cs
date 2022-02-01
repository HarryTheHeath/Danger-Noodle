using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Head : Body
{
    private MoveDirection moveDirection;
    private FoodSpawner foodSpawner;
    public Body bodyPrefab;

    private void Awake()
    {
        foodSpawner = GameObject.Find("Food Spawner").GetComponent<FoodSpawner>();
    }

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
        foodSpawner.SpawnFood();
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            MoveTo(transform.position + GetMovement(), GetRotation());
            canTurn = true;
        }
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
        foodSpawner.SpawnFood();
        Grow(bodyPrefab);
    }
}
