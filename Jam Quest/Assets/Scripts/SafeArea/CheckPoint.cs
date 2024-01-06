using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] Transform spawnTransform;
    [SerializeField] private KeyCode interactionKey;
    bool canInteract = false;

    [HideInInspector] public Vector3 respawnPoint;
    public bool isActivated = false;

    private void Awake()
    {
        respawnPoint = spawnTransform.position;
    }
    private void Update()
    {
        if (canInteract && Input.GetKeyDown(interactionKey))
        {
            if (!isActivated)
            {
                isActivated = true;
                AllCheckPoints.currentCheckpoint = this;
                FindObjectOfType<AllCheckPoints>().DeactivateLastCheckPoint();
            }
        }
    }
    public void DeactivateCheckpoint()
    {
        isActivated = false;
    }

    #region Can Interact
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
    #endregion
}
