using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] Transform spawnTransform;
    [SerializeField] private KeyCode interactionKey;
    bool canInteract = false;

    [HideInInspector] public Vector3 respawnPoint;

    [SerializeField] bool spawnFacingRight;
    [SerializeField] float spawnXPos;
    public bool isActivated = false;

    [SerializeField] GameObject downGradeCardsPrefab;
    [SerializeField] GameObject downGradeCardsSpawnPoint;
    [SerializeField] UnityEvent onDownGrade;

    private void Awake()
    {
        spawnTransform.localPosition = spawnFacingRight ? new Vector3(spawnXPos, spawnTransform.localPosition.y, spawnTransform.localPosition.z) : new Vector3(-spawnXPos, spawnTransform.localPosition.y, spawnTransform.localPosition.z);

        respawnPoint = spawnTransform.position;
    }
    private void Update()
    {
        if (canInteract && Input.GetKeyDown(interactionKey))
        {
            if (!isActivated)
            {
                SpawnDownGradeCards();
            }
        }
    }
    public void ActivateCheckPoint()
    {
        if (!isActivated)
        {
            isActivated = true;
            AllCheckPoints.currentCheckpoint = this;
            FindObjectOfType<AllCheckPoints>().DeactivateLastCheckPoint();
        }
    }
    public void DeactivateCheckpoint()
    {
        isActivated = false;
    }
    private void SpawnDownGradeCards()
    {
        Instantiate(downGradeCardsPrefab, downGradeCardsSpawnPoint.transform);
        onDownGrade?.Invoke();
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
