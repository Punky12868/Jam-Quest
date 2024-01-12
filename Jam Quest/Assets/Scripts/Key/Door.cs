using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool canInteract = false;
    [SerializeField] Vector2 openPos;
    Vector2 closedPos;

    [SerializeField] float speed = 3.5f;

    public bool isOpen = false;

    [SerializeField] BoxCollider2D trigger;
    private void Awake()
    {
        closedPos = transform.position;

        if (isOpen)
        {
            transform.position = openPos;
        }
        else
        {
            transform.position = closedPos;
        }
    }
    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerInventory.Instance.hasKey)
            {
                isOpen = true;
            }
        }

        if (isOpen && (Vector2)transform.position != openPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, openPos, speed * Time.deltaTime);
        }
        else if ((Vector2)transform.position != closedPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, closedPos, speed * Time.deltaTime);
        }

        if (isOpen && trigger.enabled)
        {
            trigger.enabled = false;
        }
        else if (!isOpen && !trigger.enabled)
        {
            trigger.enabled = true;
        }

        if (canInteract)
        {
            InteractFeedback.Instance.ShowInteractDoor(true);
        }
        else
        {
            InteractFeedback.Instance.ShowInteractDoor(false);
        }
    }

    #region CanInteract Logic

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
