using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public InputField userName;
    public InputField password; 
    public APISystem api;

    public GameObject playerLogin;
    public GameObject playerSignUp;
    public GameObject signupStatus;
 
    public void savePlayerName()
    {
        if (string.IsNullOrEmpty(userName.text))
        {
            Debug.Log("Enter the username");
            if (string.IsNullOrEmpty (password.text))
            {
                Debug.Log("Enter the username and password");
            }
        }
        if (string.IsNullOrEmpty (password.text))
        {
            Debug.Log("Enter the password");
        }
        else
        {
            PlayerPrefs.SetString("username", userName.text);
            FindObjectOfType<APISystem>().GetPlayer(userName.text);
            signupStatus.SetActive(false);
        }
    }


}
