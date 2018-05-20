using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogScript : MonoBehaviour {
    public float fogLevel;

    private int counter = 0;
    private float targetPos =-20;
    public int updateBuffer = 3;



    float currentLevel;
    public float  CurrentLevel
    {
        private get
        {
            return currentLevel;
        }

        set         
        {
            counter++;
            if (counter > updateBuffer)
                counter = 0;
            else if (counter == updateBuffer)
                targetPos =  currentLevel - fogLevel;

            currentLevel = value;

        }
    }

    private void Update()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y,targetPos,0.3f));
    }



}
