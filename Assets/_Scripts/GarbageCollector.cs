using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine(CleaningGarbage());
    }

    // Cleans garbage.
    public IEnumerator CleaningGarbage()
    {
        yield return new WaitForSeconds(10);
        print("collect");
        System.GC.Collect();
        yield return new WaitForSeconds(10);
        print("assets");
        Resources.UnloadUnusedAssets();
        StartCoroutine("LimpiasionGarbage");
    }
}
