using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakParts : MonoBehaviour
{
    public bool isLeft;
    public int fuerza;
    //Makes the different parts of the box fly off
    void Start()
    {
        Rigidbody2D rb;
        rb = GetComponent<Rigidbody2D>();
        if(isLeft)
        {
            rb.AddForce(new Vector2(-0.6f,1)* 7, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(0.6f, 1) * 7, ForceMode2D.Impulse);
        }
        
    }

    
}
