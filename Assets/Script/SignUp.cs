using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUp : MonoBehaviour
{
    public InputField firstName;
    public InputField lastName;
    public InputField userName;
    public InputField password;
    public APISystem api;

    public GameObject playerSignUp;
    public GameObject playerLogin;
    public GameObject signupStage;
    public GameObject loginStage;

    public void selectSignUp()
    {
       playerLogin.SetActive(false);
        playerSignUp.SetActive(true);
        loginStage.SetActive(false);
        signupStage.SetActive(false);
    }

    public void SignUpPlayer()
    {
        Debug.Log(userName.text);
        Debug.Log(password.text);
        Debug.Log(firstName.text);
        Debug.Log(lastName.text);
        FindObjectOfType<APISystem>().Register(userName.text, password.text, firstName.text, lastName.text);
       
    }


}
