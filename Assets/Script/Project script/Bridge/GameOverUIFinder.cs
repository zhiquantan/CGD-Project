using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameOverUIFinder : MonoBehaviour
{
    public GameObject GameOverUI;
    public GameObject Bridge;
    public TextMeshProUGUI Timetext;
    public float score;
    public TextMeshProUGUI Scoretext;
    public string name;
    public string name1;
    public GameObject[] playernumber;
    public static string difficulty;
    public float multiplier;
    // Start is called before the first frame update
    void Start()
    {
        if(difficulty=="easy")
        {
            multiplier=1;
        }

        else if(difficulty=="normal")
        {
            multiplier=2;
        }

        else if(difficulty=="hard")
        {
            multiplier=3;
        }
        name1=name;
        GameOverUI.SetActive(true);
        Bridge=GameObject.FindGameObjectsWithTag("Bridge")[0];
        
        Timetext.text=Bridge.GetComponent<Bridge>().time.ToString();
        StartCoroutine(CalculateScore());
        
        Scoretext.text=(Mathf.Round(score)).ToString();
        //Scoretext.text=name1;
    }

    IEnumerator CalculateScore()
    {
        playernumber=GameObject.FindGameObjectsWithTag("Player");
       score=((100f/Bridge.GetComponent<Bridge>().time)*100f)*multiplier/playernumber.Length;
        yield return new WaitForSeconds(1.0f);
    }

    public void goLeaderboard()
    {
        //Destroy(roomManager.Instance.gameObject);
        StartCoroutine(saveScore1());

    }

    IEnumerator saveScore1()
    {
        FindObjectOfType<APISystem>().InsertPlayerActivity(PlayerPrefs.GetString("username"), "HomeToBridge_ID", "add", (Mathf.Round(score)).ToString());
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;

        PlayerPrefs.SetString("username", name1);
        SceneManager.LoadScene(4);
    }
}
