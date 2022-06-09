using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System.Linq;
using System.IO;

public class SpawnCharacterUI : MonoBehaviour
{
    public GameObject CharacterSelectionUISpawn;
    public static GameObject CharacterSelectionUI;
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(spawnObject());
        // PhotonNetwork.Instantiate(Path.Combine("ObjectPrefabs", "ChooseCharacterUI"), CharacterSelectionUISpawn.transform.position, CharacterSelectionUISpawn.transform.rotation);
        Instantiate(CharacterSelectionUISpawn, CharacterSelectionUISpawn.transform.position, CharacterSelectionUISpawn.transform.rotation);

        CharacterSelectionUI = GameObject.FindGameObjectsWithTag("CharacterUI")[0];
        CharacterSelectionUI.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {

    }

    // IEnumerator spawnObject()
    // {
    //     PhotonNetwork.Instantiate(Path.Combine("ObjectPrefabs", "ChooseCharacterUI"), CharacterSelectionUISpawn.transform.position, CharacterSelectionUISpawn.transform.rotation);
    //     yield return new WaitForSeconds(1);
    // }
}
