using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainManuControl : MonoBehaviour {
    
    GameObject levels;
    GameObject locks;
    

        
	void Start ()
    {

        //PlayerPrefs.DeleteAll();

        levels = GameObject.Find("levels");

        locks = GameObject.Find("locks");

        for (int i = 0; i < levels.transform.childCount; i++)
        {
            levels.transform.GetChild(i).gameObject.SetActive(false);

        }
        

        for (int i = 0; i < locks.transform.childCount; i++)
        {
            locks.transform.GetChild(i).gameObject.SetActive(false);

        }

       

       

        for(int i=0; i<PlayerPrefs.GetInt("whichlevel") ; i++)
        {

            locks.transform.GetChild(i).GetComponent<Button>().interactable=true;
            
        }
    }
    public void chooseButton(int inputbutton)
    {
        if (inputbutton == 1)
        {
            if (PlayerPrefs.GetInt("whichlevel") >0)
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("whichlevel"));
                
            }
            else
            {
                SceneManager.LoadScene(1);

            }
            
        }
        else if(inputbutton == 2)
        {
            for (int i = 0; i < levels.transform.childCount; i++)
            {
                locks.transform.GetChild(i).gameObject.SetActive(true);

            }

            for (int i = 0; i < locks.transform.childCount; i++)
            {
                locks.transform.GetChild(i).gameObject.SetActive(true);

            }

            for (int i = 0; i < PlayerPrefs.GetInt("WhichLevel"); i++)
            {

                locks.transform.GetChild(i).gameObject.SetActive(false);

            }
        }
        else if (inputbutton == 3)
        {
            Application.Quit();

        }
        
    }
	public void levelsChoose(int button)
    {

        SceneManager.LoadScene(button);
       


    }
	
	
}
