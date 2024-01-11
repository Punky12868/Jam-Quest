using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] Transform spawnTransform;
    [SerializeField] private KeyCode interactionKey;
    [HideInInspector] public bool canInteract = false;

    [HideInInspector] public Vector3 respawnPoint;

    [SerializeField] bool spawnFacingRight;
    [SerializeField] float spawnXPos;
    public bool isActivated = false;

    GameObject downGradeCardsSpawnPoint;
    [SerializeField] GameObject[] downGradeCardsPrefab;
    [SerializeField] UnityEvent onDownGrade;

    private void Awake()
    {
        spawnTransform.localPosition = spawnFacingRight ? new Vector3(spawnXPos, spawnTransform.localPosition.y, spawnTransform.localPosition.z) : new Vector3(-spawnXPos, spawnTransform.localPosition.y, spawnTransform.localPosition.z);

        respawnPoint = spawnTransform.position;

        downGradeCardsSpawnPoint = GameObject.FindGameObjectWithTag("InGame").transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        if (canInteract && Input.GetKeyDown(interactionKey) && !downGradeCardsSpawnPoint.activeInHierarchy)
        {
            if (!isActivated)
            {
                SpawnDownGradeCards();
                PauseGame.Pause();
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
        onDownGrade?.Invoke();

        for (int i = 0; i < downGradeCardsPrefab.Length; i++)
        {
            Instantiate(downGradeCardsPrefab[i], downGradeCardsSpawnPoint.transform);
        }

        downGradeCardsSpawnPoint.SetActive(true);
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
