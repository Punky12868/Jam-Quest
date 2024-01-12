using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBackButton : MonoBehaviour
{
    [SerializeField] GameObject cardsHolder;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject text;

    private void Update()
    {
        if (cardsHolder.activeInHierarchy && !backButton.activeInHierarchy)
        {
            backButton.SetActive(true);
        }

        if (!cardsHolder.activeInHierarchy && backButton.activeInHierarchy)
        {
            backButton.SetActive(false);
        }

        if (cardsHolder.activeInHierarchy && !text.activeInHierarchy)
        {
            text.SetActive(true);
        }

        if (!cardsHolder.activeInHierarchy && text.activeInHierarchy)
        {
            text.SetActive(false);
        }
    }
}
