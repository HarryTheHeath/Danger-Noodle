using UnityEngine;

public class Food : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        var head = col.GetComponent<Head>();
        if (head == null)
        {
            return;
        }

        head.Eat(this);
    }
}