using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorCollision : MonoBehaviour
{
    public Animator animacion;
    public bool isEmpty;
    public int surpriseItem;
    public BoxCollider2D box;
    private MoveThings move;
    public bool isHit;
    public enum WhatThingIs
    {
        mushroomBig,
        mushroomLife,
        GoombaEnemy,
        KoopaEnemy,
        CanonEnemy,
        BreakBlock,
        SurpriseBox,
        PiranhaFlower,
        Killzone
        
    }
    public WhatThingIs whatThingIs;
    private animationElevationBlock elevationBlock;
    public GameObject player;
    public GameObject goomba;
    public bool killsEnemies;
    
    private bool isShell;
    private void Start()
    {
      
        isEmpty = false;
        elevationBlock = GetComponent<animationElevationBlock>();
        animacion = GetComponent<Animator>();
        move = GetComponent<MoveThings>();
    }
    //Activates the animation of the turtle that starts going out the shell after being hit. 
    IEnumerator LegsOut()
    {
        yield return new WaitForSeconds(6);
        killsEnemies = false;
        isShell = true;
        animacion.SetBool("IsLegs", true);
        animacion.SetBool("IsShell", false);
        yield return new WaitForSeconds(2);
        animacion.SetBool("IsLegs", false);
        box.size = new Vector2(0.16f, 0.23f);
        box.offset = new Vector2(0, 0f);
        isShell = false;
    }
    //Detects collision to different objects and characters
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Detects collision with a mushroom that makes Mario big.
        if (collision.transform.CompareTag("Player") && whatThingIs == WhatThingIs.mushroomBig)
        {
            collision.gameObject.transform.SendMessage("Big");

            Destroy(this.gameObject);
        }
        //Detects collision with the killzone that destroys everything that goes out othe game.
        if (collision.transform.CompareTag("KillZone") && whatThingIs == WhatThingIs.GoombaEnemy || whatThingIs == WhatThingIs.KoopaEnemy && collision.transform.CompareTag("KillZone")
            || whatThingIs == WhatThingIs.mushroomBig && collision.transform.CompareTag("KillZone"))
        {
           Destroy(this.gameObject,0);
        }
        if (collision.transform.CompareTag("Player") && whatThingIs == WhatThingIs.Killzone)
        {
            collision.gameObject.transform.SendMessage("Muerte");

            
        }

        //Detects collision with a Goomba
        if (collision.transform.CompareTag("Player") && whatThingIs == WhatThingIs.GoombaEnemy)
        {

            Vector3 targetDir = player.transform.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.up);

            //if Mario jumps above the Goomba and kill it.
            if(angle < 50.0f)
            {
                
                animacion.SetBool("Death", true);
                player.gameObject.GetComponent<Mario>().Jump(600,false, Vector2.up);
                GameObject kick = Instantiate(Resources.Load("KickVariant", typeof(GameObject))) as GameObject;
                Destroy(kick, 6);
                

            }
            //if Mario crashes in front the Goomba it makes him small or kill him.
            else
            {
                collision.gameObject.transform.SendMessage("Small");
               
            }
            
            
        }
        //if Mario collides the Piranha Flower it makes him small or kill him.
        if (collision.transform.CompareTag("Player") && whatThingIs == WhatThingIs.PiranhaFlower)
        {
     
            collision.gameObject.transform.SendMessage("Small");

        }
        //Detects collision with a Koopa
        if (collision.transform.CompareTag("Player") && whatThingIs == WhatThingIs.KoopaEnemy)
        {

            Vector3 targetDir = player.transform.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.up);

            //if Mario jumps above the Koopa and it goes inside his shell.
            if (angle < 50.0f && isShell == false)
            {

                box.size = new Vector2(0.12f, 0.14f);
                box.offset = new Vector2(0, 0);
                player.gameObject.GetComponent<Mario>().Jump(600, false, Vector2.up);
                GameObject kick = Instantiate(Resources.Load("KickVariant", typeof(GameObject))) as GameObject;
                Destroy(kick, 6);
                animacion.SetBool("IsShell", true);
                move.StopMove();
                isShell = true;
                StartCoroutine(LegsOut());
                killsEnemies = false;

            }
            //if Mario crashes in front of the Koopa and it is out of his shell it makes him small or kill him.
            else if (isShell == false)
            {
             
                collision.gameObject.transform.SendMessage("Small");

            }
            //if Mario collides the Koopa and it is in of his shell, the shell shoots away to the opposite direction.
            else if (isShell == true)
            {
               
                if (player.transform.position.x > transform.position.x)
                {
                   
                    move.left = true;
                    move.Dash();
                    isShell = false;
                    killsEnemies = true;
                }
                if (player.transform.position.x < transform.position.x)
                {
                  
                    move.left = false;
                    move.Dash();
                    isShell = false;
                    killsEnemies = true;

                }



            }
           




        }
        //When the Koopa shell is shot away can kill enemies.
        if (collision.transform.CompareTag("Enemy") && whatThingIs == WhatThingIs.KoopaEnemy && killsEnemies == true)
        {
            collision.gameObject.GetComponent<MoveThings>().Jump(1800, Vector2.up);
            GameObject kick = Instantiate(Resources.Load("KickVariant", typeof(GameObject))) as GameObject;
            Destroy(kick, 6);
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());
            Destroy(collision.gameObject, 6);
            
        }
        //If Mario is big can break de brick boxes.
        if (collision.transform.CompareTag("Player") && whatThingIs == WhatThingIs.BreakBlock && player.GetComponent<Mario>().isBig == true)
        {
            Vector3 targetDir1 = player.transform.position - transform.position;
            float angle1 = Vector3.Angle(targetDir1, transform.up);

            //Only if the are hitten from below.
            if(angle1 >  160f)
            {
                
                GameObject breakBox = Instantiate(Resources.Load("BreakVariant", typeof(GameObject))) as GameObject;
                Destroy(breakBox, 6);
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

                transform.GetChild(0).gameObject.SetActive(true);
                Destroy(this.gameObject,5);
            }
        }
        //if they are not hitten from below the will just elevate up and then down
        if (collision.transform.CompareTag("Player") && whatThingIs == WhatThingIs.BreakBlock && player.GetComponent<Mario>().isBig == false)
        {
            
            Vector3 targetDir1 = player.transform.position - transform.position;
            float angle1 = Vector3.Angle(targetDir1, transform.up);

            if (angle1 > 160f)
            {
                elevationBlock.BoxUpDown();
                GameObject notBreakBox = Instantiate(Resources.Load("BumpVariant", typeof(GameObject))) as GameObject;
                Destroy(notBreakBox, 6);
            }
        }
        //Detects collision for  Mario  and the Surprise Boxes , they will give coins or mushrooms if they are not empty.
        if (collision.transform.CompareTag("Player") && whatThingIs == WhatThingIs.SurpriseBox)
        {
           
            Vector3 targetDir1 = player.transform.position - transform.position;
            float angle1 = Vector3.Angle(targetDir1, transform.up);

            if (angle1 > 160f && isEmpty == false)
            {
                elevationBlock.BoxUpDown();
                animacion.SetBool("isEmpty", true);
                isEmpty = true;
               
                switch(surpriseItem)
                {
                    case 0:
                        GameObject coin = Instantiate(Resources.Load("CoineVariant", typeof(GameObject)), transform.GetChild(0).gameObject.transform.position, Quaternion.identity) as GameObject;
                        GameObject coinSound = Instantiate(Resources.Load("CoinVariant", typeof(GameObject))) as GameObject;
                        Destroy(coinSound, 6);Destroy(coin, 1);
                        break;
                    case 1:
                        GameObject life = Instantiate(Resources.Load("MushroomBigVariant", typeof(GameObject)), transform.GetChild(0).gameObject.transform.position, Quaternion.identity) as GameObject;
                        GameObject mBigSound = Instantiate(Resources.Load("ItemVariant", typeof(GameObject))) as GameObject;
                        Destroy(mBigSound, 6);
                        break;
                 


                }
            }
            //Empty boxes will just elevate.
            else if(angle1 > 160f && isEmpty == true)
            {
                elevationBlock.BoxUpDown();
                GameObject notBreakBox = Instantiate(Resources.Load("BumpVariant", typeof(GameObject))) as GameObject;
                Destroy(notBreakBox, 6);
            }
        }
    }
    
}
