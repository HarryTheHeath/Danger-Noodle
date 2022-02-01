using System;
using System.Collections;
using UnityEngine;

public class Body : MonoBehaviour
{
    protected Body body;

    public void Grow(Body body)
    {
        if (this.body != null)
        {
            this.body.Grow(body);
        }
        else
        {
            this.body = Instantiate(body, transform.position, transform.rotation);
        }
    }

    public void MoveTo(Vector3 position, Quaternion rotation)
    {
        if (body != null)
        {
            body.MoveTo(transform.position, transform.rotation);
        }
        transform.rotation = rotation;
        transform.position = position;
    }
}
