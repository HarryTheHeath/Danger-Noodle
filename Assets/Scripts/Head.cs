using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class Head : Body
{
    private MoveDirection moveDirection;
    private FoodSpawner foodSpawner;
    public Body bodyPrefab;
    private Score score;
    private GameOver gameOver;
    
    private bool Alive;
    public Sprite alive;
    public Sprite dead;
    
    public int StartSpeed = 3;
    public int MinSpeed = 1;
    public int MaxSpeed = 5;
    
    private float ActualSpeed = 0.3f;
    private int TextSpeed = 3;
    
    public GameObject SpeedTMP;
    private TMP_Text speedText;

    private void Awake()
    {
        foodSpawner = GameObject.Find("Food Spawner").GetComponent<FoodSpawner>();
        score = GameObject.Find("Game Manager").GetComponent<Score>();
        gameOver = GameObject.Find("Game Manager").GetComponent<GameOver>();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = alive;
        
        speedText = SpeedTMP.GetComponent<TMP_Text>();
        speedText.text = $"Speed: {TextSpeed}";
        Alive = true;
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
        while (Alive)
        {
            yield return new WaitForSeconds(ActualSpeed);
            MoveTo(transform.position + GetMovement(), GetRotation());
            canTurn = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        var snakeBody = col.GetComponent<Body>();
        if (snakeBody == null)
        {
            return;
        }
        Alive = false;
        Debug.Log("Ow, I ate myself...");
        gameOver.TriggerReset();
    }

    void Update()
    {
        if (!Alive)
            this.gameObject.GetComponent<SpriteRenderer>().sprite = dead;
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

        if (Input.GetKeyDown(KeyCode.W))
        {
            SetSpeed();
        }
    }

    private void SetSpeed()
    {
        if (StartSpeed != MinSpeed)
        {
            StartSpeed -= 1;
            ActualSpeed -= 0.1f;
        }
        else
        {
            StartSpeed = MaxSpeed;
            ActualSpeed = 0.5f;
        }
        SetSpeedText();
    }
    
    private void SetSpeedText()
    {
        TextSpeed = StartSpeed switch
        {
            1 => 5,
            2 => 4,
            4 => 2,
            5 => 1,
            _ => 3
        };
        
        speedText.text = $"Speed: {TextSpeed}";
    }

    public void Eat(Food food)
    {
        Destroy(food.gameObject);
        foodSpawner.SpawnFood();
        Grow(bodyPrefab);
        score.AddScore();
    }
}
