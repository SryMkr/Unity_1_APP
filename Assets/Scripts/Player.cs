using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    // it is the jianshu qiao branch now
    [SerializeField] public float moveSpeed = 5f;

    GameObject currentFloor;
    [SerializeField] int Hp;
    [SerializeField] GameObject Hpbar;
    [SerializeField] Text scoreText;
    int score;
    float scoreTime;
    Animator anim;
    [SerializeField] GameObject replayButton;

    // Start is called before the first frame update
    void Start()
    {
        Hp = 10;
        score = 0;
        scoreTime = 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
            transform.Translate(moveSpeed*Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
            anim.SetBool("run", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = true;
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
        UpdateScore();


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (collision.contacts[0].normal == new Vector2(0f, 1f))
            {
                currentFloor = collision.gameObject;
                collision.gameObject.GetComponent<AudioSource>().Play();
                ModifyHp(1);
            
            }
            
        }

        else if (collision.gameObject.CompareTag("Spike"))
        {
            if (collision.contacts[0].normal == new Vector2(0f, 1f))
            {
                currentFloor = collision.gameObject;
                anim.SetTrigger("hurt");
                ModifyHp(-3);
            }

        }

        else if (collision.gameObject.CompareTag("SpikeWall"))
        {
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            anim.SetTrigger("hurt");
            ModifyHp(-3);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("DeathLine"))
        {
            Debug.Log("you lose");
            replayButton.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void ModifyHp(int num)
    {
        Hp += num;

        if (Hp > 10)
        {
            Hp = 10;
        }
        else if (Hp < 0)
        {
            Hp = 0;
            Time.timeScale = 0f;
            replayButton.SetActive(true);
        }
        UpdateHpBar();
    }

    void UpdateHpBar()
    {
        for (int i = 0; i < Hpbar.transform.childCount; i++)
        {
            if (Hp > i)
            {
                Hpbar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                Hpbar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void UpdateScore()
    {
        scoreTime += Time.deltaTime;
        if (scoreTime > 2f)
        {
            score++;
            scoreTime = 0f;
            scoreText.text = score.ToString() + "level";
        }
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("First");
    }

   
}
