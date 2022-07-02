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
    }

    public void selectLogin()
    {
        playerLogin.SetActive(true);
        playerSignUp.SetActive(false);
    }

    public void SignUpPlayer()
    {
        FindObjectOfType<APISystem>().Register(userName.text, password.text, firstName.text, lastName.text);
        playerSignUp.SetActive(false);
        playerLogin.SetActive(true);
        signupStage.SetActive(true);
    }


}
