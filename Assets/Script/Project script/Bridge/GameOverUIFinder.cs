using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUIFinder : MonoBehaviour
{
    public GameObject GameOverUI;
    public GameObject Bridge;
    public TextMeshProUGUI Timetext;
    public int score;
    public TextMeshProUGUI Scoretext;
    // Start is called before the first frame update
    void Start()
    {
        GameOverUI.SetActive(true);
        Bridge=GameObject.FindGameObjectsWithTag("Bridge")[0];
        Timetext.text=Bridge.GetComponent<Bridge>().time.ToString();
        score=(100/Bridge.GetComponent<Bridge>().time)*100;
        Scoretext.text=score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
