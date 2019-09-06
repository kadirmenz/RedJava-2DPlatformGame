using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;


public class CharacterControl : MonoBehaviour {
    public Sprite[] waitingAnim;
    public Sprite[] jumpAnim;
    public Sprite[] walkAnim;
    public Sprite[] doorAnim;
    float doorAnimTime=0;
    int waitingAnimCounter=0;
    int walkingAnimCounter=0;
    int doorAnimCounter = 0;
    float waitingAnimTime;
    public Text doorText; 
    float walkingAnimTime;
    SpriteRenderer spriterenderer;
    public Text goldText;
    Rigidbody2D physic;
    public int howmuchGold=4;
    RigidbodyConstraints2D constraints;
    Vector3 vec;
    float horizontal = 0;
    bool jumpOnce=true;
    Vector3 cameraLastPos;
    Vector3 cameraFirstPos;
	GameObject camera;
    public Text healthText;
    int health=100;
    public Image blackBackground;
    float aCounter;
    float returnMainMenuTime;
    Sprite doorSprite;
    float z = 0;
    int goldCounter=0;
    
	void Start ()
    {
        
        Time.timeScale = 1;
        blackBackground.gameObject.SetActive(false);
        constraints = RigidbodyConstraints2D.FreezeRotation;


        spriterenderer = GetComponent<SpriteRenderer>();
        doorSprite = GameObject.FindGameObjectWithTag("door").GetComponent<SpriteRenderer>().sprite;
        
        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("WhichLevel"))
        {
            PlayerPrefs.SetInt("WhichLevel", SceneManager.GetActiveScene().buildIndex);

        }else if (PlayerPrefs.GetInt("WhichLevel") == null)
        {
            PlayerPrefs.SetInt("Kaçıncılevel", SceneManager.GetActiveScene().buildIndex);
        }
        
        physic = GetComponent<Rigidbody2D>();
        if (SceneManager.GetActiveScene().buildIndex>PlayerPrefs.GetInt("WhichLevel"))
        {
            PlayerPrefs.SetInt("WhichLevel", SceneManager.GetActiveScene().buildIndex);

        }
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        cameraFirstPos = camera.transform.position - transform.position;
        healthText.text = "HEALTH " + health;
        goldText.text = goldCounter + "/" + howmuchGold;
	}
	
	

	void Update ()
    {
        //Input.GetKeyDown(KeyCode.Space)
        if (CrossPlatformInputManager.GetButtonDown("Jump")
)
        {
            if (jumpOnce)
            {
                physic.AddForce(new Vector2(0, 500));
                jumpOnce = false;
            }
            
            
        }
	}
   


    void Die()
    {
        if (health <= 0)
        {
            Time.timeScale = 0.66f;
            healthText.enabled = false;
            aCounter += 0.03f;
            blackBackground.gameObject.SetActive(true);

            blackBackground.color = new Color(0, 0, 0, aCounter);

            
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).transform.position = transform.position;
              

                transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300, 300), Random.Range(-300, 300)));
                transform.GetChild(i).SetParent(transform.parent);

            }
            spriterenderer.enabled = false;

            returnMainMenuTime += Time.deltaTime;
            if (returnMainMenuTime > 3)
            {
                SceneManager.LoadScene("mainmenu");

            }
        }
    }
    void FixedUpdate() 
    {
       
        CharacterMove();
        Animation();
        Die();


    }
   
    void LateUpdate()
    {
        CameraControl();
        
    }
    void CharacterMove()
    {
        horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        vec = new Vector3(horizontal * 10, physic.velocity.y, 0);
        physic.velocity = vec;

    }
    void CameraControl()
    {

        cameraLastPos = cameraFirstPos + transform.position;
        camera.transform.position = Vector3.Lerp(camera.transform.position, cameraLastPos, 0.1f);

    }
    void Animation()
    {
        if (jumpOnce) {
            if (horizontal == 0)
            {
                waitingAnimTime += Time.deltaTime;

                if (waitingAnimTime > 0.05f)
                {
                    spriterenderer.sprite = waitingAnim[waitingAnimCounter++];
                    if (waitingAnimCounter == waitingAnim.Length)
                    {
                        waitingAnimCounter = 0;
                    }
                    waitingAnimTime = 0;


                }
            }
            else if (horizontal > 0)
            {
                walkingAnimTime += Time.deltaTime;

                if (walkingAnimTime > 0.01f)
                {
                    spriterenderer.sprite = walkAnim[walkingAnimCounter++];
                    if (walkingAnimCounter == walkAnim.Length)
                    {
                        walkingAnimCounter = 0;
                    }
                    walkingAnimTime = 0;

                }
                transform.localScale = new Vector3(1, 1, 1);
            }

            else if (horizontal < 0)
            {
                walkingAnimTime += Time.deltaTime;

                if (walkingAnimTime > 0.01f)
                {
                    spriterenderer.sprite = walkAnim[walkingAnimCounter++];
                    if (walkingAnimCounter == walkAnim.Length)
                    {
                        walkingAnimCounter = 0;
                    }
                    walkingAnimTime = 0;

                }
                transform.localScale = new Vector3(-1, 1, 1);

            }
        }
        else
        {
            if( physic.velocity.y > 0)
            {
                spriterenderer.sprite = jumpAnim[0];
            }
            else
            {
                spriterenderer.sprite = jumpAnim[1];

            }
            if (horizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }else if (horizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

      
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "bullet")
        {
            health -= 1;
            healthText.text = "HEALTH " + health;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            health -= 10;
            healthText.text = "HEALTH " + health;
        }
        
        if (col.gameObject.tag == "chest")
        {
            if (health < 100)
            {
                health += 10;
                healthText.text = "HEALTH " + health;
                col.gameObject.GetComponent<ChestControl>().enabled = true;
                col.GetComponent<CapsuleCollider2D>().enabled = false;
                Destroy(col.gameObject, 3);
            }
        }
        if (col.gameObject.tag == "gold")
        {

            goldCounter++;
            Destroy(col.gameObject);
            goldText.text = goldCounter + "/" + howmuchGold;
        }
        if (col.gameObject.tag == "water")
        {
            health = 0;
            
        }
        if (col.gameObject.tag == "turnover")
        {

            z = transform.rotation.z + 45;

            transform.Rotate(0, 0, z);



        }


    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Saw")
        {
            health -= 10;
            healthText.text = "HEALTH " + health;
            wait(0.5f);
        }
        if (col.gameObject.tag == "turnover")
        {
            

           
          
            z = transform.rotation.z + 45;

            transform.Rotate(0, 0, z);




        }

        if (col.gameObject.tag == "door")
        {
            
                doorAnimTime += Time.deltaTime;
                doorText.text = "Jump for enter";
                if (doorAnimTime > 0.1f)
                {

                    if (doorAnimCounter < doorAnim.Length)
                    {
                        col.gameObject.GetComponent<SpriteRenderer>().sprite = doorAnim[doorAnimCounter];
                        doorAnimCounter++;
                    }
                    doorAnimTime = 0;

                }
            //Input.GetKeyDown(KeyCode.Space)
            if (CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(wait(0.2f));
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            
        }
    }
    IEnumerator wait(float num)
    {
        yield return new WaitForSeconds(num);

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        jumpOnce = true;
       
    }
}
