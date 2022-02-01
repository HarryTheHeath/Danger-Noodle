 using UnityEngine;

public class Body : MonoBehaviour
{
    protected Body body;
    private Transform playerTransform;
    private Vector3 playerPos;
    private Quaternion playerRot;


    public void Awake()
    {
        playerTransform = transform;
        playerPos = playerTransform.position;
        playerRot = playerTransform.rotation;
    }

    public void Grow(Body body)
    {
        if (this.body != null)
        {
            this.body.Grow(body);
        }
        else
        {
            this.body = Instantiate(body, playerPos, playerRot);
        }
    }

    public void MoveTo(Vector3 position, Quaternion rotation)
    {
        if (this.body != null)
        {
            this.body.MoveTo(playerPos, playerRot);
        }
        playerRot = rotation;
        playerPos = position;
    }
}