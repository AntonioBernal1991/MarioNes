using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassGameData1

{
    public int loves;

}

public class SaveLoadMenu : MonoBehaviour
{

    public ClassGameData1 objetoGameData;

    //Creates the files and saves the info.
    void Start()
    {

        objetoGameData = new ClassGameData1();

        objetoGameData.loves = 3;


        PlayerPrefs.SetInt("Loves", objetoGameData.loves);
        PlayerPrefs.Save();
    }
}