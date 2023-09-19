using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flagtrigger : MonoBehaviour
{

    public GameObject marioTheme;

    //When mario crosses the flag wil activates a winnig song and will pas to the next level.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") )
        {
            marioTheme.SetActive(false);
            GameObject flag = Instantiate(Resources.Load("FlagAudioVariant", typeof(GameObject))) as GameObject;
            Destroy(flag, 6);
            StartCoroutine(changeScene(4));
        }
    }
    //Changes scene.
    public IEnumerator changeScene(int scene)
    {
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(scene);
    }

}
