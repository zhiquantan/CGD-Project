using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System.Linq;
using System.IO;

public class gameflow : MonoBehaviour
{
    public Transform tile1Obj;
    private Vector3 nextTileSpawn;

    public Transform bricksObj;

    public Transform LifeObj;
    private Vector3 nextBrickSpawn;

    public Transform smCrateObj;
    private Vector3 nextsmCrateObjSpawn;

    public Transform dbCrateObj;
    private Vector3 nextdbCrateObjSpawn;

    public Transform CartObj;

    private Vector3 nextCartObjSpawn;

    public Transform Coin;

    public Transform Bullet;
    private Vector3 nextCoinObjSpawn;

    float[] numbers = new float[3] { -0.1f, 0.1f, 0.3f };

    int randomIndex;
    private float randX;

    private int randChoice;
    public static int totalCoins = 0;


    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            nextTileSpawn.z = 4.2f;
            StartCoroutine(spawnTile());
        }

    }


    void Update()
    {

    }
    IEnumerator spawnTile()
    {
        yield return new WaitForSeconds(1.5f);
        randomIndex = Random.Range(0, 3);
        randX = numbers[randomIndex];
        nextBrickSpawn = nextTileSpawn;
        nextBrickSpawn.x = randX;
        nextBrickSpawn.y = 0.22f;
        //Instantiate(tile1Obj, nextTileSpawn, tile1Obj.rotation);
        PhotonNetwork.Instantiate(Path.Combine("ObjectPrefabs", "Tile"), nextTileSpawn, tile1Obj.rotation);
        //Instantiate(bricksObj, nextBrickSpawn, bricksObj.rotation);
        PhotonNetwork.Instantiate(Path.Combine("ObjectPrefabs", "Rock"), nextBrickSpawn, bricksObj.rotation);


        nextTileSpawn.z += 1.4f;
        randX = numbers[randomIndex];
        nextsmCrateObjSpawn.z = nextTileSpawn.z;
        nextsmCrateObjSpawn.y = 0.22f;
        nextsmCrateObjSpawn.x = randX;
        //Instantiate(tile1Obj, nextTileSpawn, tile1Obj.rotation);
        PhotonNetwork.Instantiate(Path.Combine("ObjectPrefabs", "Tile"), nextTileSpawn, tile1Obj.rotation);
        //Instantiate(smCrateObj, nextsmCrateObjSpawn, smCrateObj.rotation);
        PhotonNetwork.Instantiate(Path.Combine("ObjectPrefabs", "SBox"), nextsmCrateObjSpawn, smCrateObj.rotation);

        if (randX == 0.1f)
        {
            randX = 0.3f;
        }
        else if (randX == 0.3f)
        {
            randX = -0.1f;
        }

        else
        {
            randX = 0.1f;
        }
        randChoice = Random.Range(0, 7);
        if (randChoice == 0)
        {
            nextdbCrateObjSpawn.z = nextTileSpawn.z;
            nextdbCrateObjSpawn.y = 0.25f;
            nextdbCrateObjSpawn.x = randX;
            //Instantiate(dbCrateObj, nextdbCrateObjSpawn, dbCrateObj.rotation);
            PhotonNetwork.Instantiate(Path.Combine("ObjectPrefabs", "CBox"), nextdbCrateObjSpawn, dbCrateObj.rotation);

        }
        else if (randChoice == 1)
        {
            nextCartObjSpawn.z = nextTileSpawn.z;
            nextCartObjSpawn.y = 0.25f;
            nextCartObjSpawn.x = randX;
            //Instantiate(CartObj, nextCartObjSpawn, CartObj.rotation);
            PhotonNetwork.Instantiate(Path.Combine("ObjectPrefabs", "Cart"), nextCartObjSpawn, CartObj.rotation);

        }

        else if (randChoice == 2)
        {
            nextCartObjSpawn.z = nextTileSpawn.z;
            nextCartObjSpawn.y = 0.25f;
            nextCartObjSpawn.x = randX;
            //Instantiate(CartObj, nextCartObjSpawn, CartObj.rotation);
            PhotonNetwork.Instantiate(Path.Combine("ObjectPrefabs", "Chest"), nextCartObjSpawn, CartObj.rotation);

        }

        else if (randChoice == 3)
        {
            nextCartObjSpawn.z = nextTileSpawn.z;
            nextCartObjSpawn.y = 0.25f;
            nextCartObjSpawn.x = randX;
            //Instantiate(CartObj, nextCartObjSpawn, CartObj.rotation);
            PhotonNetwork.Instantiate(Path.Combine("ObjectPrefabs", "BulletObject"), nextCartObjSpawn, Bullet.rotation);

        }
        else if (randChoice == 4)
        {
            nextCartObjSpawn.z = nextTileSpawn.z;
            nextCartObjSpawn.y = 0.25f;
            nextCartObjSpawn.x = randX;
            //Instantiate(CartObj, nextCartObjSpawn, CartObj.rotation);
            PhotonNetwork.Instantiate(Path.Combine("ObjectPrefabs", "Life"), nextCartObjSpawn, LifeObj.rotation);

        }

        else if (randChoice == 5)
        {
            nextCartObjSpawn.z = nextTileSpawn.z;
            nextCartObjSpawn.y = 0.25f;
            nextCartObjSpawn.x = randX;
            //Instantiate(CartObj, nextCartObjSpawn, CartObj.rotation);
            PhotonNetwork.Instantiate(Path.Combine("ObjectPrefabs", "Potion"), nextCartObjSpawn, CartObj.rotation);

        }

        else
        {
            nextCoinObjSpawn.z = nextTileSpawn.z;
            nextCoinObjSpawn.y = 0.25f;
            nextCoinObjSpawn.x = randX;
            //Instantiate(Coin, nextCoinObjSpawn, Coin.rotation);
            PhotonNetwork.Instantiate(Path.Combine("ObjectPrefabs", "Coin"), nextCoinObjSpawn, Coin.rotation);
        }

        nextTileSpawn.z += 1.4f;
        StartCoroutine(spawnTile());
    }
}
