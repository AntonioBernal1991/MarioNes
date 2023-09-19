using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    public float velocidadBullet;

    public Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //StartCoroutine("pooling");
        //Destroy(gameObject, 5.0f);
        direction = new Vector3(1, 0, 0);
    }
   
    void Update()
    {
        rb.AddForce(direction * 10 * velocidadBullet);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.transform.CompareTag("Bullet"))
        {
            ObjectPooling.instance.ReturnBullet(this.gameObject);
            ObjectPooling.instance.ApagarBullet(this.gameObject);
        }
    }




}







     

