using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // if (PV.IsMine)
        // {
        CreateController();
        // }

    }

    void CreateController()
    {
        if (CharacterSelection.ChoosedCharacter == 0)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController 1"), Vector3.zero, Quaternion.identity);
        }

        else if (CharacterSelection.ChoosedCharacter == 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController 2"), Vector3.zero, Quaternion.identity);
        }

        else if (CharacterSelection.ChoosedCharacter == 2)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
        }

    }

    // Update is called once per frame

}
