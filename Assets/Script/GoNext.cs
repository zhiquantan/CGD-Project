using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GoNext : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }




    public void GoLeaderBoard()
    {
        //Destroy(roomManager.Instance.gameObject);
        StartCoroutine(Disconnect());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator Disconnect()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene(4);
    }
}
