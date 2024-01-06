using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] UnityEvent OnPlayerDeath;
    Vector2 respawnPoint;

    private void Awake()
    {
        respawnPoint = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.position = respawnPoint;
            ReduceLight.SetReduce(false);
            OnPlayerDeath?.Invoke();
        }
    }
}
