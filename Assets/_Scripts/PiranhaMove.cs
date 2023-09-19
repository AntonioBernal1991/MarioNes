using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaMove : MonoBehaviour
{
   
    public float speed;
    Vector3 initialPosition;
    bool isMoving;
    void Start()
    {
        initialPosition = transform.position;
        isMoving = true;
    }

   
    void Update()
    {
        StartCoroutine(MoveUpDown());
    }

    //Moves Up and down the Piranha Flower
    IEnumerator MoveUpDown()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            if (transform.position.y > initialPosition.y + 1.5)
            {

                transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
                isMoving = false;
                yield return new WaitForSeconds(2);
                isMoving = true;
                speed = speed * -1;


            }
            if (transform.position.y < initialPosition.y - 1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
                isMoving = false;
                yield return new WaitForSeconds(1);
                isMoving = true;
                speed = speed * -1;

            }

        }


    }








}
