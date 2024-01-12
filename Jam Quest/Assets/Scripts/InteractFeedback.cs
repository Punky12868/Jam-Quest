using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractFeedback : MonoBehaviour
{
    public static InteractFeedback Instance;

    bool interactCheckpointOne;
    bool interactCheckpointTwo;
    bool interactDoor;

    [SerializeField] GameObject interactFeedback;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (interactCheckpointOne || interactCheckpointTwo || interactDoor)
        {
            interactFeedback.SetActive(true);
        }
        else
        {
            interactFeedback.SetActive(false);
        }
    }

    public void ShowInteractDoor(bool value) 
    {
        interactDoor = value;
    }

    public void ShowInteractCheckpointOne(bool value)
    {
        interactCheckpointOne = value;
    }

    public void ShowInteractCheckpointTwo(bool value)
    {
        interactCheckpointTwo = value;
    }
}
