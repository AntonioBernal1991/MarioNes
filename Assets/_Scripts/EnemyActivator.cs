using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    
    public List<GameObject> gameObjects;

    //When Mario crosses an invisible collider , the enemies ahead will be activated
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            foreach (GameObject ob in gameObjects)
            {
                ob.SetActive(true);
            }
        }
       
    }
}
