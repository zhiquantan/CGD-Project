using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public Text userName;
    public APISystem api;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void savePlayerName(string sceneMenu)
    {
        if (string.IsNullOrEmpty(userName.text))
        {
            Debug.Log("Enter the username");
        }
        else
        {
            PlayerPrefs.SetString("username", userName.text);
            FindObjectOfType<APISystem>().Register(userName.text, userName.text, userName.text, userName.text);
            SceneManager.LoadScene(1);
        }
    }


}
