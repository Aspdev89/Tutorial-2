using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    public Text livesText;
    public Text winText;
    public Text loseText;
    private int scoreValue = 0;
    private int livesValue = 3;
    public bool gameOver;

public AudioClip musicClipOne;

public AudioClip musicClipTwo;

public AudioSource musicSource;

Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        livesText.text = livesValue.ToString();
         winText.text = "";
        loseText.text = "";
        gameOver = false;

        {

          musicSource.clip = musicClipOne;

          musicSource.Play();
          musicSource.loop = true;

          anim.SetInteger("State", 1);

         }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (scoreValue == 4) 
                        {
                          transform.position = new Vector3(33.0f, 0.0f);
                          livesValue = 3; 
                          livesText.text = livesValue.ToString();
                        }
                        
         if(scoreValue== 8)
          {
              gameOver = true;
               scoreValue += 1; scoreValue = 9;
                    score.text = scoreValue.ToString();
          }
          //if(scoreValue==8)
          //{
           if(gameOver == true)
          {
            winText.text = "You Win! Game by Joey DeSimone";
            livesValue = 99;
            livesText.text = livesValue.ToString();
             //Destroy(gameObject);
              {
                  musicSource.Stop();
                musicSource.clip = musicClipTwo;

                musicSource.Play();
                gameOver = false;
                scoreValue += 1; scoreValue = 9;
                    score.text = scoreValue.ToString();

         }
         }//}
      
    

    if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            livesText.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
         if(livesValue<= 0)
          {
            loseText.text = "You Lose!";
            Destroy(gameObject);
         }
      
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            }
        }
    }
}