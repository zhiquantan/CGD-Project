using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class APISystem : MonoBehaviour
{
    const string URI = "http://api.tenenet.net";
    const string token = "50560e216946467e58f7ff2067eb107e";
    const string leaderboard_id = "HomeToBridge_LeaderboardID";

    public ContainerA containerA; //user details
    public ContainerB containerB; //leaderboard details

    public InputField username;
    public InputField password;
    public GameObject LoginStage;
    public GameObject SignUpPage;
    public GameObject signupSame;
    public GameObject playerLogin;
    public GameObject signupStage;

    public void GetLeaderboard()
    {
        StartCoroutine(GetLeaderboardController());
    }


    public void GetPlayer(string alias)
    {
        StartCoroutine(GetPlayerController(alias));
    }

    public void Register(string alias, string id, string fname, string lname)
    {
        StartCoroutine(UploadController(alias, id, fname, lname));
    }

    public void InsertPlayerActivity(string alias, string metric_ID, string addOrRemove, string value)
    {
        StartCoroutine(InsertPlayerActivityController(alias, metric_ID, addOrRemove, value));
    }

    public void EnablePlayer(string alias)
    {
        StartCoroutine(EnablePlayerController(alias));
    }

    public void DisablePlayer(string alias)
    {
        StartCoroutine(DisablePlayerController(alias));
    }




    public IEnumerator UploadController(string alias, string id, string fname, string lname)
    {
        UnityWebRequest www = UnityWebRequest.Get(URI + "/createPlayer" + "?token=" + token + "&alias=" + alias + "&id=" + id + "&fname=" + fname + "&lname=" + lname);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
             containerA = JsonUtility.FromJson<ContainerA>(www.downloadHandler.text);

            Debug.Log(containerA.message.id);
            if (containerA.status == "0")
            {
                Debug.Log("Same username!");
                SignUpPage.SetActive(true);
                signupSame.SetActive(true);
            }
            else
            {

                EnablePlayer(alias);
                Debug.Log(www.downloadHandler.text);
                SignUpPage.SetActive(false);
                playerLogin.SetActive(true);
                signupStage.SetActive(true);
                signupSame.SetActive(false);
            }
            }
    }

    public IEnumerator EnablePlayerController(string alias)
    {
        UnityWebRequest www = UnityWebRequest.Get(URI + "/enablePlayer" + "?token=" + token + "&alias=" + alias);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }

    public IEnumerator DisablePlayerController(string alias)
    {
        UnityWebRequest www = UnityWebRequest.Get(URI + "/disablePlayer" + "?token=" + token + "&alias=" + alias);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }

    public IEnumerator GetPlayerController(string alias)
    {
        Debug.Log("Getting player's data...");
        UnityWebRequest www = UnityWebRequest.Get(URI + "/getPlayer" + "?token=" + token + "&alias=" + alias);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            StartCoroutine(GetPlayerController(alias));
        }
        else
        {

            Debug.Log((www.downloadHandler.text));
            containerA = JsonUtility.FromJson<ContainerA>(www.downloadHandler.text);

            Debug.Log(containerA.message.id);
            Scene scene = SceneManager.GetActiveScene();
            if(scene.name=="Login Page")
            {
                if(containerA.status == "0")
                {
                    Debug.Log("Login Fail");
                    LoginStage.SetActive(true);
                }

                else if (containerA.status == "1")
                {
                    
                    if (password.text == containerA.message.id)
                        
                        {
                        Debug.Log(username.text);
                        Debug.Log("Name :" + PlayerPrefs.GetString("username"));
                        SceneManager.LoadScene("Room");
                        LoginStage.SetActive(false);
                        }
                }
                else
                {
                    LoginStage.SetActive(true);
                    Debug.Log("Failed");
                }
            }
        }
    }

    public IEnumerator InsertPlayerActivityController(string alias, string valueToChange, string addOrRemove, string value)
    {
        Debug.Log("Inserting player's activity data...");
        UnityWebRequest www = UnityWebRequest.Get(URI + "/insertPlayerActivity" + "?token=" + token + "&alias=" + alias + "&id=" + valueToChange + "&operator=" + addOrRemove + "&value=" + value);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            StartCoroutine(InsertPlayerActivityController(alias, valueToChange, addOrRemove, value));
        }
        else
        {
            Debug.Log("Complete inserting player's activity data!");
            Debug.Log(www.downloadHandler.text);
        }
    }

    private IEnumerator GetLeaderboardController()
    {
        Debug.Log("Getting shooting leaderboard...");
        UnityWebRequest www = UnityWebRequest.Get(URI + "/getLeaderboard" + "?token=" + token + "&id=" + leaderboard_id);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            StartCoroutine(GetLeaderboardController());
        }
        else
        {
            Debug.Log("Complete getting shooting leaderboard!");
            Debug.Log(www.downloadHandler.text);
            containerB = JsonUtility.FromJson<ContainerB>(www.downloadHandler.text);

        }
    }

}
