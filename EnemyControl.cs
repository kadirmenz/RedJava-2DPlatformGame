using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class EnemyControl : MonoBehaviour
{

    GameObject[] destinations;
    Vector3[] drawingrecord;
    bool takedistanceBetw = true;
    Vector3 distanceBetw;
    GameObject java;
    int velocity = 5;
    public Sprite frontSide;
    public Sprite backSide;
    RaycastHit2D ray;
    int distanceBetwCounter = 0;
    bool gocomeCheck = true;
    SpriteRenderer spriterenderer;
    public GameObject bullet;
    public LayerMask layermask;
    float fireTime =0;
    void Start()
    {
        
        java = GameObject.FindGameObjectWithTag("Player");
        spriterenderer = GetComponent<SpriteRenderer>();
        destinations = new GameObject[transform.childCount];
        drawingrecord = new Vector3[transform.childCount];
        for (int i = 0; i < drawingrecord.Length; i++)
        {
            drawingrecord[i] = transform.GetChild(i).transform.position;
        }
        for (int i = 0; i < destinations.Length; i++)
        {
            destinations[i] = transform.GetChild(0).gameObject;
            destinations[i].transform.SetParent(transform.parent);
        }

    }


    void FixedUpdate()
    {
       
        goToPoints();




        didSee();
        if (ray.collider.tag=="Player")
        {
            spriterenderer.sprite = frontSide;
            fire();
        }
        else if (ray.collider.tag == "cantsee")
        {
            spriterenderer.sprite = backSide;
        }
        else
        {
            spriterenderer.sprite = backSide;


        }

    }
    public static Vector2 getDirection()
    {
        return (java.transform.position - transform.position).normalized;
    }
    void fire()
    {
        fireTime += Time.deltaTime;
        if (fireTime > Random.Range(0.4f, 1))
        {
            Instantiate(bullet, transform.position, Quaternion.identity);

            fireTime = 0;

        }

    }
    void didSee()
    {
        Vector3 düşmanyön = java.transform.position - transform.position;

        ray = Physics2D.Raycast(transform.position, düşmanyön, 1000, layermask);
        Debug.DrawLine(transform.position, ray.point, Color.blue);
    }

    void goToPoints()
    {
        if (takedistanceBetw)
        {
            distanceBetw = (destinations[distanceBetwCounter].transform.position - transform.position).normalized;
            takedistanceBetw = false;

        }
        float distance = Vector3.Distance(transform.position, destinations[distanceBetwCounter].transform.position);
        if (distance < 0.5f)
        {

            if (gocomeCheck)
            {
                distanceBetwCounter++;
                if (distanceBetwCounter == destinations.Length)
                {
                    distanceBetwCounter--;
                    gocomeCheck = false;
                }
                takedistanceBetw = true;
            }
            else
            {
                distanceBetwCounter--;
                if (distanceBetwCounter < 0)
                {
                    distanceBetwCounter++;
                    gocomeCheck = true;

                }
                takedistanceBetw = true;
            }



        }
        transform.position += distanceBetw * Time.deltaTime * velocity;

    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {


        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);

        }
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(EnemyControl))]
[System.Serializable]
class düşmankontrolEditör : Editor
{
    public override void OnInspectorGUI()
    {
        EnemyControl script = (EnemyControl)target;
        if (GUILayout.Button("üret", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject yeniObjem = new GameObject();
            yeniObjem.transform.parent = script.transform;
            yeniObjem.transform.position = script.transform.position;
            yeniObjem.name = script.transform.childCount.ToString();
        }
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("frontSide"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("backSide"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layermask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bullet"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }



}
#endif

