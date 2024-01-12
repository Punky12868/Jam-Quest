using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscToGoBack : MonoBehaviour
{
    [SerializeField] GameObject credits;
    [SerializeField] GameObject assets;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (credits.activeInHierarchy)
            {
                credits.SetActive(false);
            }

            if (assets.activeInHierarchy)
            {
                assets.SetActive(false);
            }
        }
    }
}
