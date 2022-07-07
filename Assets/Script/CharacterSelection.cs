using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;
using System.IO;

public class CharacterSelection : MonoBehaviour
{

    public GameObject CharacterSelectionUI;

    //public GameObject RoomMenuUI;
    public GameObject[] characters;
    public GameObject AvailableText;
    public GameObject NotAvailableText;
    public GameObject ChooseButton;
    public GameObject PlayButton;
    public int selectedCharacter = 0;
    public int PreparedPlayer = 0;
    public static string rank;
    public GameObject Top1Text;
    

    public static int ChoosedCharacter;
    private GameObject PlayerList;

    public bool[] CharacterSelected = new bool[3];

    private PhotonView PV;

    bool IPicked = false;



    Player[] players = PhotonNetwork.PlayerList;
    void Start()
    {
        PV = GetComponent<PhotonView>();

        

        for (int i = 0; i < 4; i++)
        {
            CharacterSelected[i] = true;
        }


    }
    void Update()
    {


        if (CharacterSelected[selectedCharacter] && !IPicked)
        {
            AvailableText.SetActive(true);
            NotAvailableText.SetActive(false);
            ChooseButton.SetActive(true);
            Top1Text.SetActive(false);

            if(rank!="1"&&selectedCharacter==3)
            {
            AvailableText.SetActive(false);
            NotAvailableText.SetActive(true);
            ChooseButton.SetActive(false);
            Top1Text.SetActive(true);
            }

            
        }
        else
        {
            AvailableText.SetActive(false);
            NotAvailableText.SetActive(true);
            ChooseButton.SetActive(false);
        }

        if (PreparedPlayer == players.Count() && PhotonNetwork.IsMasterClient)
        {
            PlayButton.SetActive(true);
        }
    }
    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void ChooseCharacter()
    {
        IPicked = true;
        ChoosedCharacter = selectedCharacter;
        // CharacterSelected[selectedCharacter] = false;
        // PreparedPlayer++;

        //CharacterSelectionUI.SetActive(false);
        //RoomMenuUI.SetActive(true);



        PV.RPC("RPC_Function", RpcTarget.AllBuffered, selectedCharacter);


    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(3);
    }

    [PunRPC]
    void RPC_Function(int CharNo)
    {
        CharacterSelected[CharNo] = false;
        PreparedPlayer++;
    }
}
