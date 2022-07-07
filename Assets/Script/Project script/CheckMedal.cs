﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CheckMedal : MonoBehaviour
{
    public APISystem api;
    public Transform parent;
    //public ScrollRect scrollRect;
    public GameObject prefab;
    public GameObject medal1;
    public GameObject medal2;
    public GameObject medal3;
    // public GameObject playerRankPos;

    #region Monobehaviour Callback

    void Start()
    {
        if (prefab)
        {
            ListTopPlayer();
        }
    }

    #endregion

    #region Public Method

    public void ListTopPlayer()
    {
        StartCoroutine(IEList());
    }

    #endregion

    #region IEnumerator Method

    private IEnumerator IEList()
    {
        List<GameObject> tempHold = new List<GameObject>();

        api.GetLeaderboard();

        yield return new WaitUntil(() => api.containerB.status == "1");

        for (int i = 0; i < api.containerB.message.data.Count; i++)
        {
            tempHold.Add(Instantiate(prefab, parent));
            tempHold[i].SetActive(false);
            tempHold[i].transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
            tempHold[i].transform.GetChild(1).GetComponent<Text>().text = api.containerB.message.data[i].alias;

            api.GetPlayer(api.containerB.message.data[i].alias);

            yield return new WaitUntil(() => api.containerA.status == "1");

            tempHold[i].transform.GetChild(2).GetComponent<Text>().text = api.containerA.message.score[0].value;

            api.containerA.status = "0";
        }

        for (int i = 0; i < api.containerB.message.data.Count; i++)
        {
            int highestScore = 0;
            for (int j = i; j < api.containerB.message.data.Count; j++)
            {
                int score = int.Parse(tempHold[j].transform.GetChild(2).GetComponent<Text>().text);

                if (score > highestScore)
                {
                    highestScore = score;
                    string holdName;
                    string holdScore;
                    holdName = tempHold[i].transform.GetChild(1).GetComponent<Text>().text;
                    tempHold[i].transform.GetChild(1).GetComponent<Text>().text = tempHold[j].transform.GetChild(1).GetComponent<Text>().text;
                    tempHold[j].transform.GetChild(1).GetComponent<Text>().text = holdName;
                    holdScore = tempHold[i].transform.GetChild(2).GetComponent<Text>().text;
                    tempHold[i].transform.GetChild(2).GetComponent<Text>().text = tempHold[j].transform.GetChild(2).GetComponent<Text>().text;
                    tempHold[j].transform.GetChild(2).GetComponent<Text>().text = holdScore;
                }
            }
            tempHold[i].SetActive(true);

            if (tempHold[i].transform.GetChild(1).GetComponent<Text>().text == PlayerPrefs.GetString("username"))// && i > 2)
            {
                Debug.Log("Rank"+(i + 1).ToString());
                if((i + 1).ToString()=="1")
                {
                    medal1.SetActive(true);
                    SC_FPSController.rank="1";
                    CharacterSelection.rank="1";
                }

                else if((i + 1).ToString()=="2")
                {
                    medal2.SetActive(true);
                    SC_FPSController.rank="2";
                    CharacterSelection.rank="2";
                }

                else if((i + 1).ToString()=="3")
                {
                    medal3.SetActive(true);
                    SC_FPSController.rank="3";
                    CharacterSelection.rank="3";
                }
                // playerRankPos.SetActive(false);
                // playerRankPos.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
                // playerRankPos.transform.GetChild(1).GetComponent<Text>().text = tempHold[i].transform.GetChild(1).GetComponent<Text>().text;
                //playerRankPos.transform.GetChild(2).GetComponent<Text>().text = tempHold[i].transform.GetChild(2).GetComponent<Text>().text;
                // playerRankPos.SetActive(true);
            }
        }

    }

    #endregion
}
