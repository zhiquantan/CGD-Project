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
    // Start is called before the first frame update
    void Start()
    {
        GameOverUI.SetActive(true);
        Bridge=GameObject.FindGameObjectsWithTag("Bridge")[0];
        Timetext.text=Bridge.GetComponent<Bridge>().time.ToString();
        StartCoroutine(CalculateScore());
        
        Scoretext.text=(Mathf.Round(score)).ToString();
    }

    IEnumerator CalculateScore()
    {
       score=(100f/Bridge.GetComponent<Bridge>().time)*100f;
        yield return new WaitForSeconds(1.0f);
    }

    public void goLeaderboard()
    {
        //Destroy(roomManager.Instance.gameObject);
        StartCoroutine(saveScore1());

    }

    IEnumerator saveScore1()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;

        FindObjectOfType<APISystem>().InsertPlayerActivity(PlayerPrefs.GetString("username"), "WoodPoint", "add", (Mathf.Round(score)).ToString());
        SceneManager.LoadScene(4);
    }
}
