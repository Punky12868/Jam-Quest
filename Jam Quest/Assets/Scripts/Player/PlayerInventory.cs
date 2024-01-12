using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public bool hasKey = false;
    [SerializeField] Image keyImage;

    [SerializeField] Sprite hasKeySprite;
    [SerializeField] Sprite noKeySprite;

    [SerializeField] GameObject key;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (hasKey && keyImage.sprite != hasKeySprite)
        {
            keyImage.sprite = hasKeySprite;
        }
        else if (!hasKey && keyImage.sprite != noKeySprite)
        {
            keyImage.sprite = noKeySprite;
        }

        if (hasKey && key.activeInHierarchy)
        {
            key.SetActive(false);
        }
        else if (!hasKey && !key.activeInHierarchy)
        {
            key.SetActive(true);
        }
    }
}
