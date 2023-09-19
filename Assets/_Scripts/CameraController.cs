using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject followObject;
    public Vector2 followOffset;
    public Vector2 threshold;
    public int speed;
    private Rigidbody2D rb;
    public GameObject limitLeft;
    public GameObject limitRight;
    void Start()
    {
        threshold = calculateThreshold();
        rb = followObject.GetComponent<Rigidbody2D>();
    }

   //Follow the player , taking care of the limits to the player and its speed.
    void FixedUpdate()
    {
        if(followObject.GetComponent<Mario>().isDead == false)
        {

            Vector2 follow = followObject.transform.position;
            float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
            float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

            Vector3 newPosition = transform.position;
            if (Mathf.Abs(xDifference) >= threshold.x)
            {
                if (follow.x - 8 > limitLeft.transform.position.x + 1 )
                {
                    newPosition.x = follow.x;
                }

            }
          
            float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);


        }
        


    }
    //Calculates the limit of the camer frame.
    private Vector3 calculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;


    }
    //Draws Lines so the developer can see the threshold on the scene editor.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}
