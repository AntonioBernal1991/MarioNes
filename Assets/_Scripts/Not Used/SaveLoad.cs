using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassGameData

{
    public int lives;

}

public class SaveLoad : MonoBehaviour
{

    public ClassGameData objetoGameData;
 

    void Start()
    {

        objetoGameData = new ClassGameData();

        objetoGameData.lives= 3;

    }
}