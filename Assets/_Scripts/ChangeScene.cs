using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
 
    public void NextScene()
    {
        StartCoroutine(TheNext());
        
    }
    //Changes scene.
    IEnumerator TheNext()
    {
        SceneManager.LoadScene(2);
      
        yield return new WaitForSeconds(1);
     

    }
}
