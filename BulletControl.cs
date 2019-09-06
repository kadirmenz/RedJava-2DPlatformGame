using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {
    Rigidbody2D physic;
    GameObject enemy;
    EnemyControl enemyControl;
    double deletedTime;
    void Start ()
    {
        physic = GetComponent<Rigidbody2D>();
        enemy = GameObject.FindGameObjectWithTag("enemy");
        enemyControl = enemy.GetComponent<EnemyControl>();
        physic.AddForce(EnemyControl.getDirection() * 1000);






    }
	
	
	void FixedUpdate ()
    {

        deletedTime += Time.deltaTime ;
        if (deletedTime > 2)
        {
            
            Destroy(GameObject.FindGameObjectWithTag("Bullet"));
        }

    }
}
