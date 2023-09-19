using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFollow : MonoBehaviour
{
    public GameObject followObject;
    public Vector2 followOffset;
    public Vector2 threshold;
    public int speed;
    private Rigidbody2D rb;
  
    void Start()
    {
        threshold = calculateThreshold();
        rb = followObject.GetComponent<Rigidbody2D>();
    }

    //Makes a limit that follows the character.
    void FixedUpdate()
    {
        

        Vector2 follow = followObject.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
      

        Vector3 newPosition = transform.position;
        if (Mathf.Abs(xDifference) >= threshold.x)
        {
            if (follow.x - 15 > transform.position.x )
            {
                newPosition.x = follow.x;
            }

        }
      
        float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);


    }

    //Calculates the limits from the camera center to the a limit so that Mario cant go backwards.
    private Vector3 calculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;


    }
   
}
