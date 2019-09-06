using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour {



    public Sprite[] goldAnim;
    SpriteRenderer spriterenderer;

    int goldCounter = 0;
    float goldTime = 0;

    void Start ()
    {
        spriterenderer = GetComponent<SpriteRenderer>();

    }
	
	
	void FixedUpdate ()
    {
        goldTime += Time.deltaTime;
        if (goldTime > 0.03f)
        {
            
            spriterenderer.sprite = goldAnim[goldCounter];
            goldCounter++;
            if (goldCounter == goldAnim.Length)
            {
                goldCounter = 0;

            }

            
            goldTime = 0;
        }
    }
}
