using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class World1 : MonoBehaviour
{
    public TextMeshProUGUI livesScore;
    public int changeSceneNumber;

   //Shows the remaining lives on the UI.
    void Start()
    {
 
        livesScore.text = " " + PlayerPrefs.GetInt("Loves");
    }

  
    void Update()
    {
        StartCoroutine(JumpScene());
    }
    //Changes scene.
    IEnumerator JumpScene()
    {
    
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(changeSceneNumber);
    }

}
