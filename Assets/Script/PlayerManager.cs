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
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController 0"), new Vector3(89f,43.2f,244.8f), Quaternion.identity);
        }

        else if (CharacterSelection.ChoosedCharacter == 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController 1"), new Vector3(171.6f,43.2f,244.8f), Quaternion.identity);
        }

        else if (CharacterSelection.ChoosedCharacter == 2)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), new Vector3(113f,43.2f,208f), Quaternion.identity);
        }

        else if (CharacterSelection.ChoosedCharacter == 3)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController 2"), new Vector3(113f,43.2f,208f), Quaternion.identity);
        }

    }

    // Update is called once per frame

}
