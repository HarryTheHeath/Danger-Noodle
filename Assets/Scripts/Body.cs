using UnityEngine;

public class Body : MonoBehaviour
{
    protected Body body;
    private Transform curBodyPart;
    private Transform PrevBodypart;


    public void Grow(Body body)
    {
        if (this.body != null)
        {
            this.body.Grow(body);
        }
        else
        {
            this.body = Instantiate(body, new Vector3(transform.position.x -1, transform.position.y,
                transform.position.z), transform.rotation);
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
