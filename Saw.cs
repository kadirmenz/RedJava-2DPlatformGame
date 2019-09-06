using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Saw : MonoBehaviour {

    GameObject[] destinations;
    Vector3[] drawingrecord;
    bool takethedistance = true;
    Vector3 distancebetween;

    int distancebetwCounter = 0;
    bool gocomeCheck = true;

	void Start ()
    {
        destinations = new GameObject[transform.childCount];
        drawingrecord = new Vector3[transform.childCount];
        for( int i =0; i< drawingrecord.Length; i++)
        {
            drawingrecord[i] = transform.GetChild(i).transform.position;
        }
        for(int i = 0; i <destinations.Length  ; i++)
        {
            destinations[i] = transform.GetChild(0).gameObject;
            destinations[i].transform.SetParent(transform.parent);
        }

    }

        
    void FixedUpdate ()
    {
        transform.Rotate(0, 0, 5);
        goToPoints();
        
        
	}

    void goToPoints()
    {
        if (takethedistance)
        {
            distancebetween = (destinations[distancebetwCounter].transform.position - transform.position).normalized;
            takethedistance = false;
           
        }
        float distance = Vector3.Distance(transform.position, destinations[distancebetwCounter].transform.position);
        if (distance < 0.5f)
        {
           
            if (gocomeCheck)
            {
                distancebetwCounter++;
                if (distancebetwCounter == destinations.Length)
                {
                    distancebetwCounter--;
                    gocomeCheck = false;
                }
                takethedistance = true;
            }
            else
            {
                distancebetwCounter--;
                if (distancebetwCounter < 0)
                {
                    distancebetwCounter++;
                    gocomeCheck = true;

                }
                takethedistance = true;
            }
           
          
           
        }
        transform.position += distancebetween * Time.deltaTime * 10;

    }

#if UNITY_EDITOR
      void OnDrawGizmos()
    {
       
        
        for (int i = 0; i <transform.childCount ; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position,transform.GetChild(i+1).transform.position);
          
        }
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(Saw))]
[System.Serializable]
class sawEditör : Editor
{
    public override void OnInspectorGUI()
    {
        Saw script = (Saw)target;
        if (GUILayout.Button("üret",GUILayout.MinWidth(100),GUILayout.Width(100)))
        {
            GameObject newObject = new GameObject();
            newObject.transform.parent = script.transform;
            newObject.transform.position = script.transform.position;
            newObject.name = script.transform.childCount.ToString();
        }
    }



}
#endif

