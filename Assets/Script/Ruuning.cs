using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using System;

public class Ruuning : MonoBehaviour
{
    private bool Immortal;
    public GameObject InvincibleEffect;
    public GameObject Bullet;

    public GameObject LifePointView;

    public int LifePoint = 1;
    public GameObject BulletNumberView;
    private string laneChange = "n";
    private string midJump = "n";
    public static Vector3 doodPos;
    public GameObject YourScore;

    public GameObject Camera;

    public GameObject ScoreText;


    public float scoreData;

    public GameObject scoreview;

    public GameObject GameOverUI;

    public GameObject Canvas;

    public GameObject CreatedGameOverUI;

    public GameObject goGameMenu;

    public GameObject goLeaderboard;

    float pointIncreasePerSecond;

    float speed;

    float jumpspeed = 0;

    float slidespeed = 0;
    bool ramp = false;

    bool dropdown = false;

    public AudioSource coins;
    public AudioSource chest;
    public AudioSource reload;
    public AudioSource life;
    public AudioSource invincible;



    // Vector3 GOUIposition=new Vector3(0,0,0);

    public GameObject Tile;

    public GameObject GameMenuLeaderboard;
    PhotonView PV;
    // Start is called before the first frame update

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        if (!PV.IsMine)
        {
            GetComponentInChildren<Camera>().enabled = false;
        }


        StartCoroutine(GenerateGameOverUI());


        //GameMenuLeaderboard = GameObject.FindGameObjectsWithTag("GameMenuLeaderBoard")[0];
        LifePointView = GameObject.FindGameObjectsWithTag("Life")[0];
        scoreview = GameObject.FindGameObjectsWithTag("ScoreView")[0];
        BulletNumberView = GameObject.FindGameObjectsWithTag("BulletNumber")[0];
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0.7f);


        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {

        GameOverUI.GetComponent<GameMenuLeaderboard>().scoreData = scoreData;
        if (scoreData < 50 && gameObject.GetComponent<Ruuning>().enabled && !ramp && !dropdown)
        {
            speed = 0.7f;
            pointIncreasePerSecond = 1;
            scoreData += pointIncreasePerSecond * Time.deltaTime;
            GetComponent<Rigidbody>().velocity = new Vector3(slidespeed, jumpspeed, speed);
            Camera.GetComponent<canMove>().CameraSpeed = new Vector3(0, 0, speed);
        }

        else if (scoreData < 150 && gameObject.GetComponent<Ruuning>().enabled && !ramp && !dropdown)
        {
            speed = 0.8f;
            pointIncreasePerSecond = 2;
            scoreData += pointIncreasePerSecond * Time.deltaTime;
            GetComponent<Rigidbody>().velocity = new Vector3(slidespeed, jumpspeed, speed);
            Camera.GetComponent<canMove>().CameraSpeed = new Vector3(0, 0, speed);
        }

        else if (scoreData < 300 && gameObject.GetComponent<Ruuning>().enabled && !ramp && !dropdown)
        {
            speed = 0.9f;
            pointIncreasePerSecond = 3;
            scoreData += pointIncreasePerSecond * Time.deltaTime;
            GetComponent<Rigidbody>().velocity = new Vector3(slidespeed, jumpspeed, speed);
            Camera.GetComponent<canMove>().CameraSpeed = new Vector3(0, 0, speed);
        }

        else if ((scoreData >= 300 && gameObject.GetComponent<Ruuning>().enabled && !ramp && !dropdown))
        {
            speed = 1.0f;
            pointIncreasePerSecond = 5;
            scoreData += pointIncreasePerSecond * Time.deltaTime;
            GetComponent<Rigidbody>().velocity = new Vector3(slidespeed, jumpspeed, speed);
            Camera.GetComponent<canMove>().CameraSpeed = new Vector3(0, 0, speed);
        }

        if (!PV.IsMine)
            return;
        if (LifePoint != 2)
        {
            LifePointView.GetComponent<Text>().text = LifePoint.ToString();
        }
        else
        {
            LifePointView.GetComponent<Text>().text = LifePoint.ToString() + " (Max)";
        }
        BulletNumberView.GetComponent<Text>().text = Bullet.GetComponent<Shooting>().bulletNumber.ToString();
        scoreview.GetComponent<Text>().text = Math.Round(scoreData).ToString();
        doodPos = transform.position;
        if (Input.GetKey("a") && (laneChange == "n") && (transform.position.x > -0.15))
        {
            slidespeed = -0.2f;
            GetComponent<Rigidbody>().velocity = new Vector3(slidespeed, 0, speed);
            laneChange = "y";
            StartCoroutine(stopLaneChh());
        }

        if (Input.GetKey("d") && laneChange == "n" && (transform.position.x < 0.15))
        {
            slidespeed = 0.2f;
            GetComponent<Rigidbody>().velocity = new Vector3(slidespeed, 0, speed);
            laneChange = "y";
            StartCoroutine(stopLaneChh());
        }
        if (Input.GetKey("space") && (midJump == "n"))
        {
            jumpspeed = 0.3f;
            GetComponent<Rigidbody>().velocity = new Vector3(0, jumpspeed, speed);
            midJump = "y";
            StartCoroutine(StopJump());
        }
    }

    IEnumerator StopJump()
    {
        yield return new WaitForSeconds(0.75f);
        jumpspeed = -0.3f;
        GetComponent<Rigidbody>().velocity = new Vector3(0, jumpspeed, speed);
        yield return new WaitForSeconds(0.75f);
        jumpspeed = 0;
        GetComponent<Rigidbody>().velocity = new Vector3(0, jumpspeed, speed);
        midJump = "n";
    }

    IEnumerator stopLaneChh()
    {
        yield return new WaitForSeconds(1);
        slidespeed = 0;
        GetComponent<Rigidbody>().velocity = new Vector3(slidespeed, 0, speed);
        laneChange = "n";


    }

    private void OnTriggerEnter(Collider other)
    {
        if (PV.IsMine)
        {
            if (other.tag == "obstacle")
            {
                other.GetComponent<AudioSource>().Play();
                if (LifePoint == 0 && !Immortal)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    Camera.GetComponent<canMove>().CameraSpeed = new Vector3(0, 0, 0);
                    //Camera.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    Debug.Log("Ouch!");
                    GameOverUI.SetActive(true);
                    GameOverUI.transform.localScale = new Vector3(1, 1, 1);
                    ScoreText.GetComponent<Text>().text = "Your Score : " + Math.Round(scoreData).ToString();
                    Tile.GetComponent<CleanUp>().enabled = false;
                    GameOverUI.GetComponent<GameMenuLeaderboard>().scoreData = scoreData;
                    gameObject.GetComponent<Ruuning>().enabled = false;
                }

                else if (!Immortal)
                {
                    LifePoint--;
                }


            }

            if (other.tag == "coin")
            {
                chest.Play();
                scoreData = scoreData + 3;
                //GameMenuLeaderboard.GetComponent<GameMenuLeaderboard>().scoreData = scoreData;
            }

            if (other.tag == "bulletObject")
            {
                reload.Play();
                Bullet.GetComponent<Shooting>().bulletNumber++;
                //GameMenuLeaderboard.GetComponent<GameMenuLeaderboard>().scoreData = scoreData;
            }

            if (other.tag == "goldBox")
            {
                coins.Play();

                scoreData = scoreData + 6;
                //GameMenuLeaderboard.GetComponent<GameMenuLeaderboard>().scoreData = scoreData;
            }

            if (other.tag == "ramp")
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 1, speed);
                ramp = true;

            }

            if (other.tag == "dropdown")
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, -1, speed);
                ramp = true;
            }

            if (other.tag == "LifeObject")
            {
                if (LifePoint < 2)
                {
                    life.Play();
                    LifePoint++;
                }
            }


        }
        if (other.tag == "InvincibleObject")
        {
            invincible.Play();
            StartCoroutine(Invincible());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ramp")
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);
            ramp = false;
        }

        if (other.tag == "dropdown")
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);
            ramp = false;
        }

    }

    IEnumerator Invincible()
    {
        Immortal = true;
        InvincibleEffect.SetActive(true);
        yield return new WaitForSeconds(5f);
        InvincibleEffect.SetActive(false);
        Immortal = false;
    }

    // IEnumerator saveScore1()
    // {
    //     yield return new WaitForSeconds(1f);
    //     PhotonNetwork.Disconnect();
    //     FindObjectOfType<APISystem>().InsertPlayerActivity(PlayerPrefs.GetString("username"), "score_point-Quan", "add", scoreData.ToString());
    //     SceneManager.LoadScene(3);
    // }

    // IEnumerator saveScore2()
    // {
    //     yield return new WaitForSeconds(1f);
    //     PhotonNetwork.Disconnect();
    //     FindObjectOfType<APISystem>().InsertPlayerActivity(PlayerPrefs.GetString("username"), "score_point-Quan", "add", scoreData.ToString());
    //     SceneManager.LoadScene(1);
    // }

    // public void goLeaderboard()
    // {

    //     StartCoroutine(saveScore1());

    // }

    // public void goGameMenu()
    // {
    //     StartCoroutine(saveScore2());

    // }
    IEnumerator Wait()
    {

        yield return new WaitForSeconds(1f);
        //CreatedGameOverUI.SetActive(false);

    }

    IEnumerator GenerateGameOverUI()
    {
        if (PV.IsMine)
        {
            yield return new WaitForSeconds(0.1f);
            //Instantiate(GameOverUI, new Vector3(0, 0, 0), GameOverUI.transform.rotation);
            //CreatedGameOverUI = GameObject.FindGameObjectsWithTag("GameOverUI")[0];

            Canvas = GameObject.FindGameObjectsWithTag("Canvas")[0];
            GameOverUI.transform.SetParent(Canvas.transform, true);
            GameOverUI.transform.localPosition = Vector3.zero;
            // goGameMenu = GameObject.FindGameObjectsWithTag("GoGameMenu")[0];
            // goGameMenu.GetComponent<Button>().interactable = true;
            // goGameMenu.GetComponent<Button>().onClick.AddListener(GameMenuLeaderboard.GetComponent<GameMenuLeaderboard>().goGameMenu);
            // goLeaderboard = GameObject.FindGameObjectsWithTag("GoLeaderboard")[0];
            // goLeaderboard.GetComponent<Button>().interactable = true;
            // goLeaderboard.GetComponent<Button>().onClick.AddListener(GameMenuLeaderboard.GetComponent<GameMenuLeaderboard>().goLeaderboard);
            // ScoreText = GameObject.FindGameObjectsWithTag("ScoreText")[0];
            // CreatedGameOverUI.SetActive(false);
        }
    }
}
