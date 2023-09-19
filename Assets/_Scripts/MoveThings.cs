using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThings : MonoBehaviour
{
    public enum WhatThingIs
    {
        mushroomBig,
        mushroomLife,
        goombaEnemy,
        koopaEnemy,
        canonEnemy,
    }
    public float velocity;

    public WhatThingIs whatThingsIs;

    private Rigidbody2D Rigid;
    public bool left;
    
    private void Start()
    {
        Rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        MovimientoObject();
        Physics2D.IgnoreLayerCollision(6, 7);
    }

    //Moves objects and character left and right.
    public void MovimientoObject()
    {
        if(left)
        {
            Rigid.velocity = new Vector2(-1 * velocity ,Rigid.velocity.y - 0.25f);
            transform.localScale = new Vector3(-7.0f, 7.0f, 7.0f);

        }
        else
        {
            Rigid.velocity = new Vector2(1 * velocity, Rigid.velocity.y - 0.25f);
            transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);
        }
        
    }

    //Stops the move
    public void StopMove()
    {
        velocity = 0;
    }
    //Throws away an object or character.
    public void Dash()
    {
        velocity = 16;
    }

    //Starts the movement again.
    public void ContinueMove()
    {
        velocity = 2;
    }

    //Changes direction of the Koopa shele when it is dashing away and collides with an enemy.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Enemy")    && whatThingsIs == WhatThingIs.koopaEnemy && velocity > 10)
        {
            left = false ;
        }
     
        else if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Obstacle") || collision.transform.CompareTag("Koopa") && velocity < 10 )
        {
            left = !left;
        }
    }

    //Makes the enemy/object jump.
    public void Jump(float _fuerzaSalto, Vector2 dir)
    {


        Rigid.AddForce(dir * _fuerzaSalto);
       
    }
}
