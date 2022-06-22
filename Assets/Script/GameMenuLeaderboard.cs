using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using System;

public class GameMenuLeaderboard : MonoBehaviour
{
    public float scoreData;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator saveScore1()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;

        FindObjectOfType<APISystem>().InsertPlayerActivity(PlayerPrefs.GetString("username"), "WoodPoint", "add", Math.Round(scoreData).ToString());
        SceneManager.LoadScene(4);
    }

    IEnumerator saveScore2()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;

        FindObjectOfType<APISystem>().InsertPlayerActivity(PlayerPrefs.GetString("username"), "WoodPoint", "add", Math.Round(scoreData).ToString());
        SceneManager.LoadScene(1);
    }

    public void goLeaderboard()
    {
        //Destroy(roomManager.Instance.gameObject);
        StartCoroutine(saveScore1());

    }

    public void goGameMenu()
    {
        //Destroy(roomManager.Instance.gameObject);
        StartCoroutine(saveScore2());

    }

}
