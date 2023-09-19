using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class animationElevationBlock : MonoBehaviour
{
    public Vector3 InitialBoxPosition;
    public float BoxElevation;


    public void Start()
    {
        InitialBoxPosition = transform.position;
    }

    public void BoxUpDown()
    {
        StartCoroutine("_BoxUpDown");
    }
    //Makes the box go up and down 
    public IEnumerator _BoxUpDown()
    {

       

        Vector3 positionDestinyUp = InitialBoxPosition + new Vector3(0f, BoxElevation, 0f);
        Vector3 positionDestinyDown = InitialBoxPosition;

        float elapsedTime = 0f;
        float duration = 0.2f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(InitialBoxPosition, positionDestinyUp, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        yield return new WaitForSeconds(0.2f);

        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(positionDestinyUp, positionDestinyDown, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
   
}

