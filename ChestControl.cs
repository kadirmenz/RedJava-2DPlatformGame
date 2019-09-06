using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestControl : MonoBehaviour {

    public Sprite[] chestAnim;
    SpriteRenderer spriterenderer;

    int chestCounter=0;
    float chestTime=0;




	void Start ()
    {
        spriterenderer = GetComponent<SpriteRenderer>();




	}
  
   

    void FixedUpdate ()
    {
        chestTime += Time.deltaTime;
        if (chestTime > 0.1f)
        {
            if (chestCounter < chestAnim.Length)
            {
                spriterenderer.sprite = chestAnim[chestCounter];
                chestCounter++;

            }
            chestTime = 0;
        }
    }
}
