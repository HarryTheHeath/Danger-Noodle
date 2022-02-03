using UnityEngine;

public class KillWall : MonoBehaviour
{
    private Head head;

    private void Awake()
    {
        head = GameObject.Find("Shnek face").GetComponent<Head>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        var headCol = col.GetComponent<Head>();
        if (headCol == null)
        {
            return;
        }
        head.KillSnake();
    }
}
