using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gameover : MonoBehaviour
{
  
    public int changeSceneNumber;
    public int seconds;

    void Update()
    {
        StartCoroutine(JumpScene(seconds));
    }

    //Changes scene to the gameover scene.
    IEnumerator JumpScene(int second)
    {

        yield return new WaitForSeconds(second);
        SceneManager.LoadScene(changeSceneNumber);
    }

}