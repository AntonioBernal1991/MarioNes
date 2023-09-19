using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{


    public static ObjectPooling instance;

    public GameObject bulletPrefab;
    public int poolSize;



    public List<GameObject> poolOfBullets = new List<GameObject>();
   
    private void Awake()
    {
  
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);  
        }
        

        for(int i = 0; i < poolSize;i++)
        {

            GameObject bulletInstanciada = Instantiate(bulletPrefab, transform.position, Quaternion.identity, this.transform);
            bulletInstanciada.transform.name = transform.name + i;
            poolOfBullets.Add(bulletInstanciada);
            ApagarBullet(poolOfBullets[i]);

        }
        
    }
  
    public void ApagarBullet(GameObject bulletInst)
    {
        bulletInst.GetComponent<MeshRenderer>().enabled = false;
        bulletInst.GetComponent<BoxCollider>().enabled = false;
        bulletInst.GetComponent<Rigidbody>().useGravity = false;
        bulletInst.GetComponent<Rigidbody>().isKinematic = true;
        bulletInst.GetComponent<Bullet>().enabled = false;

        bulletInst.transform.position = new Vector3(0, 0, 0);

    }

    public void GetBullet(GameObject disparador)
    {
        //usingBullets.Add(poolOfBullets[0]);
       

        poolOfBullets[0].transform.position = disparador.transform.GetChild(0).gameObject.transform.position;

        poolOfBullets[0].GetComponent<MeshRenderer>().enabled = true;
        poolOfBullets[0].GetComponent<BoxCollider>().enabled = true;
        //bullets[indiceBala].GetComponent<Rigidbody>().useGravity = true;
        poolOfBullets[0].GetComponent<Rigidbody>().isKinematic = false;
        poolOfBullets[0].GetComponent<Bullet>().enabled = true;
        poolOfBullets.Remove(poolOfBullets[0]);
    }
    public void ReturnBullet(GameObject balaRetornar)
    {
        poolOfBullets.Add(balaRetornar);
    }
}
