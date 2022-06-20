using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollideBridge : MonoBehaviour
{
    public Image BridgeLife;
    public GameObject Bridge;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Animal")
        {
            if(Bridge.GetComponent<Bridge>().CurrentPhase!=0)
            {
                BridgeLife.fillAmount=BridgeLife.fillAmount-0.1f;
            }
            Destroy(other.gameObject);
        }
    }
}
