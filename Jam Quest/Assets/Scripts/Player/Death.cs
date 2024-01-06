using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] float reduceLightDistance;
    [SerializeField] UnityEvent OnPlayerDeath;
    Vector2 respawnPoint;

    [SerializeField] private KeyCode kys;

    private void Awake()
    {
        ResetSpawnPoint();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnDeath();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(kys))
        {
            OnDeath();
        }

        bool reduce = transform.position.x > respawnPoint.x - reduceLightDistance && transform.position.x < respawnPoint.x + reduceLightDistance;
        if (!reduce)
        {
            ReduceLight.SetReduce(true);
        }
    }

    private void OnDeath()
    {
        transform.position = respawnPoint;
        ReduceLight.SetReduce(false);
        OnPlayerDeath?.Invoke();
    }

    public void SetRespawnPoint(Vector2 point)
    {
        respawnPoint = point;
    }

    public void ResetSpawnPoint()
    {
        respawnPoint = transform.position;
    }
}
