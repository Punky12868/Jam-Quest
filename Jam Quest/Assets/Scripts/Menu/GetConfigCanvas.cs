using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetConfigCanvas : MonoBehaviour
{
    public void GetConfig()
    {
        GameObject.FindGameObjectWithTag("ConfigMenu").transform.GetChild(0).gameObject.SetActive(true);
    }
}
