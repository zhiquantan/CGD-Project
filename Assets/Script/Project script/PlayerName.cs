using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
       Name = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.GetComponent<SC_FPSController>().Name!=null)
        {
            Name.text=Player.GetComponent<SC_FPSController>().Name;
        }
    }
}
