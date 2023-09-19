using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    
  
  //Destroys the dead character.
  public void DeathFunction()
    {

        Destroy(this.gameObject);
    }
}
